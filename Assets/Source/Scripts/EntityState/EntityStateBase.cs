public abstract class EntityStateBase
{
    public abstract EntityStateID EntityStateID { get; }
    public abstract void OnStateEnter();
    public abstract void OnStateExit();
    public virtual void OnUpdate() { }
    public virtual void OnFixedUpdate() { }
}