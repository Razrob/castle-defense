using System;

public interface IUnit
{
    public UnitID UnitID { get; }
    public UnitType UnitType { get; }
    public IHealth Health { get; }
    public bool IsDied { get; }

    public void Return();

    public event Action<UnitBase, IDamageApplicator> OnUnitDied;
    public event Action<IDamagable, IDamageApplicator> OnDamageTake;
}
