using UnityEngine;

public class AttackUnitsAttackState : EntityStateBase
{
    private EntityStateID _entityStateID = EntityStateID.Attack;
    private Animator _animator;
    private AttackUnit _unit;
    private float _actionSpeed = 0;

    public override EntityStateID EntityStateID => _entityStateID;

    public AttackUnitsAttackState(AttackUnit attackUnit)
    {
        _unit = attackUnit;
        _animator = attackUnit.Animator;
    }

    public override void OnStateEnter()
    {
        _unit.SetDestination(_unit.transform.position);
        _actionSpeed = 0;
        _animator.Play("Attack");
    }

    public override void OnUpdate() { }

    public override void OnFixedUpdate()
    {
        _animator.SetFloat("ActionSpeed", _actionSpeed);
        if (_actionSpeed<1)
            _actionSpeed  += Mathf.Lerp(0, 1,3*Time.deltaTime);
    }

    public override void OnStateExit() { }
}


