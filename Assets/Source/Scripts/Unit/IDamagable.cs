using System;

public interface IDamagable
{
    public IHealth Health { get; }
    public event Action<IDamagable, IDamageApplicator> OnDamageTake;
    public void TakeDamage(IDamageApplicator damageApplicator);
}
