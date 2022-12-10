using System;
using System.Collections.Generic;
using System.Reflection;

public interface IUnitRepository
{
    public IReadOnlyDictionary<UnitType, List<UnitBase>> Units { get; }

    public void AddUnit(UnitBase unit);
    public TUnit TryGetUnit<TUnit>(UnitType unitType, Predicate<TUnit> predicate = null, bool remove = false) where TUnit : UnitBase;

    public event Action<UnitBase> OnUnitAdd;
    public event Action<UnitBase> OnUnitRemove;
    public event Action<UnitBase> OnUnitDied;
}

public class UnitRepository : IUnitRepository
{
    private readonly Dictionary<UnitType, List<UnitBase>> _units;
    public IReadOnlyDictionary<UnitType, List<UnitBase>> Units => _units;

    public event Action<UnitBase> OnUnitAdd;
    public event Action<UnitBase> OnUnitRemove;
    public event Action<UnitBase> OnUnitDied;

    public UnitRepository()
    {
        _units = new Dictionary<UnitType, List<UnitBase>>(5);
    }

    public void AddUnit(UnitBase unit)
    {
        if (!_units.ContainsKey(unit.UnitType))
            _units.Add(unit.UnitType, new List<UnitBase>(5));

        _units[unit.UnitType].Add(unit);
        unit.OnUnitDied += UnitDied;
    }

    public TUnit TryGetUnit<TUnit>(UnitType unitType, Predicate<TUnit> predicate = null, bool remove = false) where TUnit : UnitBase
    {
        if (!_units.TryGetValue(unitType, out List<UnitBase> units))
            return default;

        int index = units.IndexOf(unit => unit.CastPossible<TUnit>() && (predicate is null || predicate(unit.Cast<TUnit>())));

        if (index is -1)
            return default;

        TUnit unit = units[index].Cast<TUnit>();

        if (remove)
        {
            units[index].OnUnitDied -= UnitDied;
            units.RemoveAt(index);
            OnUnitRemove?.Invoke(unit);
        }

        return unit;
    }

    private void UnitDied(UnitBase unit, IDamageApplicator damageApplicator)
    {
        unit.OnUnitDied -= UnitDied;
        OnUnitDied(unit);
    }
}

