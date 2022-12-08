using System;
using UnityEngine;

[Serializable]
public struct UnitConfiguration<TUnit> where TUnit : IUnit
{
    [SerializeField] private TUnit _unit;

    public UnitConfiguration(TUnit unit)
    {
        _unit = unit;
    }

    public TUnit Unit => _unit;

    public static implicit operator UnitConfiguration<IUnit>(UnitConfiguration<TUnit> configuration)
    {
        return new UnitConfiguration<IUnit>(configuration.Unit);
    }
}