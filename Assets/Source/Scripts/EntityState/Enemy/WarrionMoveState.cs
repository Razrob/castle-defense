using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WarrionMoveState : EntityStateBase
{
    private EntityStateID _entityStateID = EntityStateID.Move;
    public override EntityStateID EntityStateID => _entityStateID;

    private Animator _animator;
    private Vector3 _velocity => _attackUnit.Velocity;
    private AttackUnit _attackUnit;

    public WarrionMoveState(AttackUnit attackUnit)
    {
        _attackUnit = attackUnit;
        _animator = attackUnit.Animator;
    }

    public override void OnStateEnter()
    {
        _animator.Play("Movement");
       
    }

    public override void OnUpdate()
    {
        _attackUnit.SetDestination(FWC.GlobalData.PlayerData.Player.Transform.position);
        _animator.SetFloat("Speed", Mathf.Lerp(0, _velocity.magnitude, 0.3f));
    }

    public override void OnStateExit()
    {

    }
}
