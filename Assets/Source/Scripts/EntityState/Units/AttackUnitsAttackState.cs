using UnityEngine;

public class AttackUnitsAttackState : UnitEntityStateBase
{
    private readonly AttackUnit _unit;

    private float _actionSpeed = 0;

    private const int ATTACK_LAYER_INDEX = 1;

    public override EntityStateID EntityStateID => EntityStateID.Attack;

    public AttackUnitsAttackState(AttackUnit attackUnit) : base(attackUnit.Animator)
    {
        _unit = attackUnit;
    }

    public override void OnStateEnter()
    {
        _actionSpeed = 0f;

        ChangeLayerWeight(ATTACK_LAYER_INDEX, 1f);
        _unit.SetDestination(_unit.transform.position);
        _unit.Animator.SetInteger("AttackType", 0);
    }

    public override void OnStateExit()
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
