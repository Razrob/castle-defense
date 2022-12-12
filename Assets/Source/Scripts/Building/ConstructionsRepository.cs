﻿using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ConstructionsRepository : IConstructionGrid
{
    private readonly BuildingGridConfig _constructionConfig;
    private readonly Dictionary<Vector3Int, ConstructionCellData> _constructions;

    public IReadOnlyCollection<Vector3Int> Positions => _constructions.Keys;
    public IReadOnlyDictionary<Vector3Int, ConstructionCellData> Constructions => _constructions;

    public event Action OnRepositoryChange;
    public event Action<ConstructionCellData> OnAdd;
    public event Action<ConstructionCellData> OnRemove;

    public ConstructionsRepository()
    {
        _constructionConfig = ConfigsRepository.FindConfig<BuildingGridConfig>() ??
            throw new NullReferenceException();

        _constructions = new Dictionary<Vector3Int, ConstructionCellData>();
    }

    public bool AnyConstructionInBuilding() => 
        _constructions.Values.Any(c => c.Construction.ActivityState is ConstructionActivityState.Building_In_Progress);

    public bool ConstructionExist(Vector3Int position)
    {
        return _constructions.ContainsKey(position);
    }
    
    public bool ConstructionExist<TType>(Vector3Int position) where TType : IConstruction
    {
        return ConstructionExist(position) && GetConstruction(position) is TType;
    }

    public void AddConstruction(Vector3Int position, ConstructionBase construction)
    {
        if (_constructions.ContainsKey(position))
            throw new Exception($"Position {position} already exist in grid");

        construction.transform.position = position;
        _constructions.Add(position, new ConstructionCellData(construction));

        OnAdd?.Invoke(_constructions[position]);
        OnRepositoryChange?.Invoke();
    }

    public ConstructionBase GetConstruction(Vector3Int position, bool remove = false)
    {
        if (!_constructions.ContainsKey(position))
            throw new Exception($"Position {position} not found");

        ConstructionCellData data = _constructions[position];

        if (remove)
        {
            _constructions.Remove(position);
            OnRemove?.Invoke(data);
        }

        return data.Construction;
    }

    public Vector3 RoundPositionToGrid(Vector3 position)
    {
        return position.Round(_constructionConfig.GridTileSize);
    }
}
