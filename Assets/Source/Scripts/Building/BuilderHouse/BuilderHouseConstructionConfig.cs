using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BuilderHouseConstructionConfig", menuName = "Config/Construction/BuilderHouseConstructionConfig")]
public class BuilderHouseConstructionConfig : ScriptableObject, ISingleConfig
{
    [SerializeField] private Vector3 _builderHousePosition;
    [SerializeField] private Vector3Int[] _busyCells;
    [SerializeField] private ConstructionConfiguration<BuilderHouseConstruction> _prefab;

    public Vector3 BuilderHousePosition => _builderHousePosition;
    public IReadOnlyList<Vector3Int> BusyCells => _busyCells;
    public ConstructionConfiguration<BuilderHouseConstruction> Prefab => _prefab;
}