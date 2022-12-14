using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ConstructionsRepository : IConstructionGrid
{
    private readonly BuildingGridConfig _constructionConfig;
    private readonly Dictionary<Vector3Int, ConstructionCellData> _constructions;
    private readonly HashSet<Vector3Int> _blockedCells;

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
        _blockedCells = new HashSet<Vector3Int>();
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

    public bool CellIsBlocked(Vector3Int position) => _blockedCells.Contains(position);
    public void BlockCell(Vector3Int position)
    {
        if (!_constructions.ContainsKey(position))
            _blockedCells.Add(position);
    }

    public void UnblockCell(Vector3Int position) => _blockedCells.Remove(position);

    public bool CellIsAvailable(Vector3Int position) 
    {
        return !ConstructionExist(position)
            && !CellIsBlocked(position)
            && _constructionConfig.GridArea.PointInArea(position);
    }

    public void AddConstruction(Vector3Int position, ConstructionBase construction)
    {
        if (!_constructionConfig.GridArea.PointInArea(new Vector2Int(position.x, position.y)))
            throw new Exception($"Position {position} out of grid area");

        if (_constructions.ContainsKey(position))
            throw new Exception($"Position {position} already exist in grid");

        if (_blockedCells.Contains(position))
            throw new Exception($"Position {position} blocked");

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

    public TConstruction GetConstruction<TConstruction>(Predicate<TConstruction> predicate = null, bool remove = false) 
        where TConstruction : IConstruction
    {
        foreach (Vector3Int position in _constructions.Keys)
        {
            if (_constructions[position].Construction.TryCast(out TConstruction construction))
                if (predicate is null || predicate(construction))
                    return GetConstruction(position, remove).Cast<TConstruction>();
        }

        return default;
    }

    public Vector3 RoundPositionToGrid(Vector3 position)
    {
        return position.Round(_constructionConfig.GridTileSize);
    }
}
