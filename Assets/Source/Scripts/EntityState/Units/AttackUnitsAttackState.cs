using UnityEngine;
using DG.Tweening.Plugins.Options;
using DG.Tweening.Core;
using System.Runtime.CompilerServices;

public class AttackUnitsAttackState : UnitEntityStateBase
{
    private EntityStateID _entityStateID = EntityStateID.Attack;
    private Animator _animator;
    private AttackUnit _unit;
    private float _actionSpeed = 0;

    private const int ATTACK_LAYER_INDEX = 1;

    public override EntityStateID EntityStateID => _entityStateID;

    public AttackUnitsAttackState(AttackUnit attackUnit) : base(attackUnit.Animator)
    {
        _unit = attackUnit;
        _animator = attackUnit.Animator;
    }

    public override void OnStateEnter()
    {
        ChangeLayerWeight(ATTACK_LAYER_INDEX, 1f);

        _unit.SetDestination(_unit.transform.position);
        _actionSpeed = 0;
        _animator.Play("Attack");
        _animator.SetInteger("AttackType", 0);
    }

    public override void OnStateExit()
    {
        ChangeLayerWeight(ATTACK_LAYER_INDEX, 0f);
        _animator.SetInteger("AttackType", -1);
    }

    public override void OnUpdate() { }

    public override void OnFixedUpdate()
    {
        _animator.SetFloat("ActionSpeed", _actionSpeed);
        if (_actionSpeed<1)
            _actionSpeed  += Mathf.Lerp(0, 1,3*Time.deltaTime);
    }
}
