using System.Collections.Generic;
using UnityEngine;

public abstract class AttackUnit : UnitBase
{
    public sealed override UnitType UnitType => UnitType.AttackUnit;
    public ConstructionBase NearestConstruction { get; private set; }
}
