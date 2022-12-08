using System;
using System.Collections.Generic;
using Zenject;

public class AttackUnitsFactoryBehaviour : UnitFactoryBehaviourBase
{
    [Inject] private UnitConfig _unitConfig;

    public override UnitType UnitType => UnitType.AttackUnit;

    public override TUnit CreateSolid<TUnit>(UnitID unitID)
    {
        UnitConfiguration<UnitBase> configuration = _unitConfig.GetConfiguration(unitID);
        UnitBase unit = Instantiate(configuration.Unit);
        return unit.Cast<TUnit>();
    }

    public override UnitConfiguration<IUnit> GetConfiguration(UnitID unitID)
    {
        return _unitConfig.GetConfiguration(unitID);
    }
}
