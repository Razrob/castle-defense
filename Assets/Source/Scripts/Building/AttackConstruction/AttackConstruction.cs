using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public abstract class AttackConstruction : ConstructionBase
{
    [SerializeField] private AttackUnitTriggerBehaviour _triggerBehaviour;

    private List<AttackUnit> _closesUnits = new List<AttackUnit>();
    public IReadOnlyList<AttackUnit> ClosestUnits => _closesUnits;
    public AttackUnit NearestUnit { get; private set; }


    [ExecuteHierarchyMethod(HierarchyMethodType.On_Awake)]
    private void OnAwake()
    {
        _triggerBehaviour.EnterEvent += component => _closesUnits.Add((AttackUnit)component);
        _triggerBehaviour.ExitEvent += component => _closesUnits.Remove((AttackUnit)component);
    }

    [ExecuteHierarchyMethod(HierarchyMethodType.On_Update)]
    private void OnUpdate()
    {
        RefreshNearestUnit();
    }

    private void RefreshNearestUnit()
    {
        NearestUnit = ClosestUnits
            .Where(unit => !unit.IsDied)
            .FindMin((last, next) => Vector3.Distance(transform.position, last.transform.position) >
                Vector3.Distance(transform.position, next.transform.position));
    }
}
