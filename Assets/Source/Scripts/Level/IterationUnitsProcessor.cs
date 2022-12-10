using System;
using System.Collections.Generic;
using System.Linq;

public class IterationUnitsProcessor
{
    private readonly List<IUnit> _units;
    private readonly LevelConfig _levelConfig;

    public int AttackIterationIndex { get; private set; }

    public event Action OnActiveUnitsDied;
    public event Action OnAllUnitsOnLevelDied;
    
    public IterationUnitsProcessor(LevelConfig levelConfig)
    {
        _units = new List<IUnit>();
        _levelConfig = levelConfig;

        AttackIterationIndex = -1;
    }

    public void AddUnit(IUnit unit, int unitAttackIterationIndex)
    {
        if (unit.IsDied)
            return;

        AttackIterationIndex = unitAttackIterationIndex;

        _units.Add(unit);
        unit.OnUnitDied += OnUnitDied;
    }

    public bool ActiveUnitsDied() => _units.All(unit => unit.IsDied);
    public bool AllUnitsOnLevelDied() => _levelConfig.CommonUnitsCount <= _units.Count(unit => unit.IsDied);

    private void OnUnitDied(UnitBase unit, IDamageApplicator damageApplicator)
    {
        unit.OnUnitDied -= OnUnitDied;

        if (ActiveUnitsDied())
            OnActiveUnitsDied?.Invoke();

        if (AllUnitsOnLevelDied())
            OnAllUnitsOnLevelDied?.Invoke();
    }
}
