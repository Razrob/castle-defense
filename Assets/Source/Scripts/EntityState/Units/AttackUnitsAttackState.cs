public class AttackUnitsAttackState : EntityStateBase
{
    private EntityStateID _entityStateID = EntityStateID.Attack;
    public override EntityStateID EntityStateID => _entityStateID;

    public AttackUnitsAttackState(AttackUnit attackUnit)
    {
     
    }

    public override void OnStateEnter() { }

    public override void OnUpdate() { }

    public override void OnFixedUpdate() { }

    public override void OnStateExit() { }
}


