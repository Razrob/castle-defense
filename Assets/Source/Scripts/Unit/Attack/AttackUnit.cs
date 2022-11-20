using System.Collections.Generic;
using UnityEngine;

public abstract class AttackUnit : UnitBase
{
   [SerializeField] private ConstructionTriggerBehaviour _triggerBehaviour;

    private List<ConstructionBase> _closesConstructions = new List<ConstructionBase>();

    public sealed override UnitType UnitType => UnitType.AttackUnit;
    public IReadOnlyList<ConstructionBase> ClosesConstructions => _closesConstructions;

    public ConstructionBase NearestConstruction { get; private set; }

    protected void OnInit()
    {
        _triggerBehaviour.EnterEvent += component => _closesConstructions.Add((ConstructionBase)component);
        _triggerBehaviour.ExitEvent += component => _closesConstructions.Remove((ConstructionBase)component);
    }
}
