using System;

public interface IUnit
{
    public UnitID UnitID { get; }
    public UnitType UnitType { get; }
    public IHealth Health { get; }
    public bool IsDied { get; }

    public event Action<UnitBase, IDamageApplicator> OnUnitDied;
    public event Action<IDamagable, IDamageApplicator> OnDamageTake;
}
