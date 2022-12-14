using System;
using System.Collections.Generic;
using UnityEngine;

public interface IConstructionGrid
{
    public IReadOnlyCollection<Vector3Int> Positions { get; }
    public bool ConstructionExist(Vector3Int position);
    public bool ConstructionExist<TType>(Vector3Int position) where TType : IConstruction;
    public bool CellIsBlocked(Vector3Int position);
    public void BlockCell(Vector3Int position);
    public void UnblockCell(Vector3Int position);
    public bool CellIsAvailable(Vector3Int position);
    public ConstructionBase GetConstruction(Vector3Int position, bool remove = false);
    public TConstruction GetConstruction<TConstruction>(Predicate<TConstruction> predicate = null, bool remove = false)
        where TConstruction : IConstruction;

    public Vector3 RoundPositionToGrid(Vector3 position);
}