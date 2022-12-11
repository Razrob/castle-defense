using UnityEngine;

public class AttackUnitsDieState : UnitEntityStateBase
{
    private readonly Animator _animator;
    private readonly AttackUnit _unit;

    private float _actionSpeed = 0;

    private const int DIE_LAYER_INDEX = 3;

    public override EntityStateID EntityStateID => EntityStateID.Die;

    public AttackUnitsDieState(AttackUnit attackUnit) : base(attackUnit.Animator)
    {
        _unit = attackUnit;
        _animator = attackUnit.Animator;
    }

    public override void OnStateEnter()
    {
        _actionSpeed = 0f;

        _unit.SetRotation(Quaternion.LookRotation(-_unit.LastDamageApplicator.DamageDirection.XZ()));

        ChangeLayerWeight(DIE_LAYER_INDEX, 1f);
        _animator.SetInteger("DieType", 0);
    }

    public override void OnStateExit()
    {
        _unit.Animator.transform.localPosition = Vector3.zero;

        ChangeLayerWeight(DIE_LAYER_INDEX, 0f);
        _animator.SetInteger("DieType", -1);
    }

    public override void OnFixedUpdate()
    {
        _animator.SetFloat("ActionSpeed", _actionSpeed);
        if (_actionSpeed < 1f)
            _actionSpeed += Mathf.Lerp(0f, 1f, Time.deltaTime * 3f);
    }
}
