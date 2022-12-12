using System;
using UnityEngine;

public class AttackUnitsAttackState : AttackUnitsAttackStateBase
{
    private readonly AttackUnit _unit;

    private float _actionSpeed = 0;

    private const int ATTACK_LAYER_INDEX = 1;
    private const float ATTACK_START_DELAY = 0.9456f;
    private const float ATTACK_INTERVAL = 2.4f;

    public override EntityStateID EntityStateID => EntityStateID.Warrior_Attack;

    public override event Action OnUnitAttack;

    public AttackUnitsAttackState(AttackUnit attackUnit) : base(attackUnit)
    {
        _unit = attackUnit;
    }

    public override void OnStateEnterCallback()
    {
        _actionSpeed = 0f;

        RegisterCycleAttackCallback(ATTACK_START_DELAY, ATTACK_INTERVAL, () => OnUnitAttack?.Invoke());

        ChangeLayerWeight(ATTACK_LAYER_INDEX, 1f);
        _unit.SetDestination(_unit.transform.position);
        _unit.Animator.SetInteger("AttackType", 0);
    }

    public override void OnStateExitCallback()
    {
        ChangeLayerWeight(ATTACK_LAYER_INDEX, 0f);
        _unit.Animator.SetInteger("AttackType", -1);
    }

    public override void OnUpdate() 
    {
        if ((_unit.NavMeshAgent.destination - _unit.transform.position).magnitude > 0.2f)
        {
            _unit.SetRotation(Quaternion.Slerp(_unit.transform.rotation,
                Quaternion.LookRotation(_unit.NavMeshAgent.destination - _unit.transform.position), Time.deltaTime * 6f));
        }

        _unit.Animator.SetFloat("ActionSpeed", _actionSpeed);

        if (_actionSpeed < 1f)
            _actionSpeed += Mathf.Lerp(0f, 1f, Time.deltaTime * 3f);
    }
}
