using UnityEngine;
using UnityEngine.AI;

public abstract class UnitMoveStateBase : UnitEntityStateBase
{
    public UnitMoveStateBase(Animator animator) : base(animator) { }

    protected bool PathIsValid(Vector3 objectPosition, Vector3 targetPosition, NavMeshPath path)
    {
        NavMesh.CalculatePath(objectPosition, targetPosition, NavMesh.AllAreas, path);

        return path.status == NavMeshPathStatus.PathComplete;
    }

    protected ConstructionPathInfo GetNearestPathToConstruction<TypeConstruction>(AttackUnit unit, ConstructionsRepository constructionsRepository) where TypeConstruction : IConstruction
    {
        ConstructionPathInfo costructionPathInfo = ConstructionPathInfo.Default;

        float? minimalDistance = null;
        foreach (Vector3Int constructionPosition in constructionsRepository.Positions)
        {
            if (constructionsRepository.ConstructionExist<TypeConstruction>(constructionPosition))
            {
                ConstructionBase construction = constructionsRepository.GetConstruction(constructionPosition);
                float distance = (unit.transform.position - construction.transform.position).magnitude;
                
                if ((!minimalDistance.HasValue || distance < minimalDistance) 
                    && (PathIsValid(unit.transform.position, constructionPosition, costructionPathInfo.Path)))
                {
                    if (PathIsValid(unit.transform.position, constructionPosition, costructionPathInfo.Path))
                    { 
                        minimalDistance = distance;
                        costructionPathInfo = new ConstructionPathInfo(constructionPosition, costructionPathInfo.Path, construction);
                    }
                }   
            }
        }

        return costructionPathInfo;
    }
}