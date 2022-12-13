using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class AttackUnitsMoveState : UnitMoveStateBase
{
    public override EntityStateID EntityStateID => EntityStateID.Move;

    private Animator _animator;
    private Vector3 _velocity => _unit.Velocity;
    private AttackUnit _unit;
    private ConstructionsRepository _constructionsRepository;

    public ConstructionPathInfo ConstructionPathInfo { get; private set; }

    private const int MOVE_LAYER_INDEX = 0;

    public AttackUnitsMoveState(AttackUnit attackUnit) : base(attackUnit.Animator) 
    {
        _unit = attackUnit;
        _animator = attackUnit.Animator;

        FWC.GlobalData.ConstructionsRepository.OnRemove += OnConstructionRemove;
    }

    private void OnConstructionRemove(ConstructionCellData data)
    {
        if (ConstructionPathInfo.Construction == data.Construction)
        {
            ConstructionPathInfo = ConstructionPathInfo.Default;
        }
    }

    public override void OnStateEnter()
    {
        _animator.SetLayerWeight(MOVE_LAYER_INDEX, 1f);
        _constructionsRepository = FWC.GlobalData.ConstructionsRepository;
    }

    public override void OnStateExit()
    {
        _animator.SetLayerWeight(MOVE_LAYER_INDEX, 0f);
    }

    public override void OnUpdate() 
    {
        FindTarget();
        _animator.SetFloat("MoveSpeed", _velocity.magnitude);
    }

    private void FindTarget()
    {
        ConstructionPathInfo pathInfo = GetNearestPathToConstruction<ConstructionBase>(_unit, _constructionsRepository);
        
        if (pathInfo.Path.status == NavMeshPathStatus.PathComplete)
        {
            RefreshPathInfo(pathInfo);
        }
        //else
        //{
        //    pathInfo = GetNearestPathToConstruction<DefenceWallConstruction>(_unit, _constructionsRepository);

        //    if (pathInfo.Path.status == NavMeshPathStatus.PathComplete)
        //        RefreshPathInfo(pathInfo);
        //}
    }

    private void RefreshPathInfo(ConstructionPathInfo pathInfo)
    {
        _unit.NavMeshAgent.path = pathInfo.Path;
        ConstructionPathInfo = pathInfo;
    }
}
