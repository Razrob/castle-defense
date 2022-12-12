using UnityEngine;
using UnityEngine.AI;

public struct ConstructionPathInfo
{
    public Vector3Int? ConstructionPosition { get; private set; }
    public NavMeshPath Path { get; private set; }
    public ConstructionBase Construction { get; private set; }

    public static ConstructionPathInfo Default => new ConstructionPathInfo(null, new NavMeshPath(), null);

    public ConstructionPathInfo(Vector3Int? costructionPosition, NavMeshPath path, ConstructionBase construction)
    {
        Path = path;
        ConstructionPosition = costructionPosition;
        Construction = construction;
    }
}
