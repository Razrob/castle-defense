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

    private CostructionPathInfo _costructionPathInfo;
    public CostructionPathInfo CostructionPathInfo => _costructionPathInfo;

    private const int MOVE_LAYER_INDEX = 0;

    public AttackUnitsMoveState(AttackUnit attackUnit) : base(attackUnit.Animator) 
    {
        _unit = attackUnit;
        _animator = attackUnit.Animator;
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

    public override void OnUpdate() { }

    public override void OnFixedUpdate()
    {
        FindTarget();
        _animator.SetFloat("MoveSpeed", _velocity.magnitude);
    }

    private void FindTarget()
    {
        CostructionPathInfo pathInfo = GetNearestPathToConstruction<AttackConstruction>(_unit, _constructionsRepository);
        
        if (pathInfo.Path.status == NavMeshPathStatus.PathComplete)
        {
            RefreshPathInfo(pathInfo);
        }
        else
        {
            pathInfo = GetNearestPathToConstruction<DefenceWallConstruction>(_unit, _constructionsRepository);
            if (pathInfo.Path.status == NavMeshPathStatus.PathComplete)
                RefreshPathInfo(pathInfo);
        }
    }

    private void RefreshPathInfo(CostructionPathInfo pathInfo)
    {
        _unit.NavMeshAgent.path = pathInfo.Path;
        _costructionPathInfo = pathInfo;
    }
}
