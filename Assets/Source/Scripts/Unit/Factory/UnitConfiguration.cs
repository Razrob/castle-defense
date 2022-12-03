using System;
using UnityEngine;

[Serializable]
public struct UnitConfiguration<TUnit> where TUnit : IUnit
{
    [SerializeField] private TUnit _unit;

    public TUnit Unit => _unit;
}