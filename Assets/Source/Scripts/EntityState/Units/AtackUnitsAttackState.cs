public class AtackUnitsAttackState : EntityStateBase
{
    private EntityStateID _entityStateID = EntityStateID.Attack;
    public override EntityStateID EntityStateID => _entityStateID;

    public override void OnStateEnter() { }

    public override void OnUpdate() { }

    public override void OnFixedUpdate() { }

    public override void OnStateExit() { }
}


