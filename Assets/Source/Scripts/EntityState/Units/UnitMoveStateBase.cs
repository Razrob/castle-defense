using UnityEngine;
using UnityEngine.AI;

public struct CostructionPathInfo
{
    private Vector3Int? _constructionPosition;
    private NavMeshPath _path;

    public Vector3Int? ConstructionPosition => _constructionPosition;
    public NavMeshPath Path => _path;

    public CostructionPathInfo(Vector3Int? costructionPosition, NavMeshPath path)
    {
        _path = path;
        _constructionPosition = costructionPosition;
    }
}

public abstract class UnitMoveStateBase : EntityStateBase
{
    protected bool PathIsValid(Vector3 objectPosition, Vector3 targetPosition, NavMeshPath path)
    {
        NavMesh.CalculatePath(objectPosition, targetPosition, NavMesh.AllAreas, path);

        return path.status == NavMeshPathStatus.PathComplete;
    }

    protected CostructionPathInfo GetNearestPathToConstruction<TypeConstruction>(AttackUnit unit, ConstructionsRepository constructionsRepository) where TypeConstruction : IConstruction
    {
        NavMeshPath pathToTarget  = new NavMeshPath();
        float? minimalDistance = null;
        Vector3Int? nearestConstructionPosition = null;
        foreach (Vector3Int constructionPosition in constructionsRepository.Positions)
        {
            if (constructionsRepository.ConstructionExist<TypeConstruction>(constructionPosition))
            {
                ConstructionBase construction = constructionsRepository.GetConstruction(constructionPosition);
                float distance = (unit.transform.position - construction.transform.position).magnitude;
                
                if ((!minimalDistance.HasValue || distance < minimalDistance)&& (PathIsValid(unit.transform.position, constructionPosition, pathToTarget)))
                {
                    if (PathIsValid(unit.transform.position, constructionPosition, pathToTarget))
                    { 
                        minimalDistance = distance;
                        nearestConstructionPosition = constructionPosition;
                    }
                }   
            }
        }
        return new  CostructionPathInfo(nearestConstructionPosition, pathToTarget);
    }
}