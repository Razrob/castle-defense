using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public interface IUnitFactory
{
    public UnitConfiguration<IUnit> GetConfiguration(UnitID unitID);
    public TUnit CreateSolid<TUnit>(UnitID unitID)
        where TUnit : IUnit;
}


public class UnitFactory : MonoBehaviour, IUnitFactory
{
    [Inject] private readonly UnitTypeMatchConfig _unitTypeMatchConfig;

    private IReadOnlyDictionary<UnitType, UnitFactoryBehaviourBase> _behaviours;

    private void Awake()
    {
        _behaviours = GetComponentsInChildren<UnitFactoryBehaviourBase>(true)
            .ToDictionary(behaviour => behaviour.UnitType, behaviour => behaviour);

        foreach (UnitFactoryBehaviourBase behaviour in _behaviours.Values)
            Debug.Log($"Factory behaviour {behaviour.GetType()} has been registered");
    }

    private void ThrowNotFoundException(UnitID unitID)
    {
        throw new InvalidOperationException($"{unitID} cannot be created, " +
            $"because factory for this unity not found. Create new factory behaviour for this unit");
    }

    public UnitConfiguration<IUnit> GetConfiguration(UnitID unitID)
    {
        UnitType unitType = _unitTypeMatchConfig.GetUnitType(unitID);

        if (!_behaviours.ContainsKey(unitType))
            ThrowNotFoundException(unitID);

        return _behaviours[unitType].GetConfiguration(unitID);
    }

    public TUnit CreateSolid<TUnit>(UnitID unitID) where TUnit : IUnit
    {
        UnitType unitType = _unitTypeMatchConfig.GetUnitType(unitID);

        if (!_behaviours.ContainsKey(unitType))
            ThrowNotFoundException(unitID);

        return _behaviours[unitType].CreateSolid<TUnit>(unitID);
    }
}
