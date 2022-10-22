using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class AttackUnit : UnitBase
{
    [SerializeField] private ConstructionTriggerBehaviour _triggerBehaviour;

    public sealed override UnitType UnitType => UnitType.AttackUnit;

    private List<ConstructionBase> _closesConstructions = new List<ConstructionBase>();
    public IReadOnlyList<ConstructionBase> ClosesConstructions => _closesConstructions;

    public ConstructionBase NearestConstruction { get; private set; }

    protected override void OnAwake()
    {
        _triggerBehaviour.EnterEvent += component => _closesConstructions.Add((ConstructionBase)component);
        _triggerBehaviour.ExitEvent += component => _closesConstructions.Remove((ConstructionBase)component);
    }

}
