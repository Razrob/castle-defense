using System;

public interface IConstruction
{
    public ConstructionLevel ConstructionLevel { get; }
    public ConstructionActivityState ActivityState { get; }
    public ConstructionID ConstructionID { get; }
    public IHealth Health { get; }

    public event Action<ConstructionBase> OnActivityStateChange;
    public event Action<IDamagable, IDamageApplicator> OnDamageTake;

    public void SetActivityState(ConstructionActivityState activityState);
    public void TakeDamage(IDamageApplicator damageApplicator);
}