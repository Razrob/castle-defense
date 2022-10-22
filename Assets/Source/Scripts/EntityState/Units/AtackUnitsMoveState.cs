using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AtackUnitsMoveState : UnitMoveStateBase
{
    private EntityStateID _entityStateID = EntityStateID.Move;
    public override EntityStateID EntityStateID => _entityStateID;

    private Animator _animator;
    private Vector3 _velocity => _unit.Velocity;
    private AttackUnit _unit;
    private ConstructionsRepository _constructionsRepository;

    private CostructionPathInfo _costructionPathInfo;
    public CostructionPathInfo CostructionPathInfo => _costructionPathInfo;

    public AtackUnitsMoveState(AttackUnit attackUnit)
    {
        _unit = attackUnit;
        _animator = attackUnit.Animator;
    }

    public override void OnStateEnter()
    {
        _animator.Play("Movement");
        _constructionsRepository = FWC.GlobalData.ConstructionsRepository;
    }

    public override void OnUpdate() { }

    public override void OnFixedUpdate()
    {
        FindTarget();
        _animator.SetFloat("Speed", _velocity.magnitude);
    }

    public override void OnStateExit() { }

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


