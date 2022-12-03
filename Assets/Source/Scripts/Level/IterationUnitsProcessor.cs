using System;
using System.Collections.Generic;
using System.Linq;

public class IterationUnitsProcessor
{
    private readonly List<IUnit> _activeUnits;
    private readonly LevelConfig _levelConfig;

    public int AttackIterationIndex { get; private set; }

    public event Action OnActiveUnitsDied;
    public event Action OnAllUnitsOnLevelDied;
    
    public IterationUnitsProcessor(LevelConfig levelConfig)
    {
        _activeUnits = new List<IUnit>();
        _levelConfig = levelConfig;

        AttackIterationIndex = -1;
    }

    public void AddUnit(IUnit unit, int unitAttackIterationIndex)
    {
        if (unit.IsDied)
            return;

        AttackIterationIndex = unitAttackIterationIndex;

        _activeUnits.Add(unit);
        unit.OnUnitDied += OnUnitDied;
    }

    public bool ActiveUnitsDied() => _activeUnits.All(unit => unit.IsDied);
    public bool AllUnitsOnLevelDied() => _levelConfig.CommonUnitsCount <= _activeUnits.Count(unit => unit.IsDied);

    private void OnUnitDied(UnitBase unit)
    {
        unit.OnUnitDied -= OnUnitDied;
        _activeUnits.Remove(unit);

        if (ActiveUnitsDied())
            OnActiveUnitsDied?.Invoke();

        if (AllUnitsOnLevelDied())
            OnAllUnitsOnLevelDied?.Invoke();
    }
}
