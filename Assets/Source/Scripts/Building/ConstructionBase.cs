using System;
using UnityEngine;

public abstract class ConstructionBase : MonoBehaviour, IConstruction, ITriggerable
{
    public ConstructionActivityState ActivityState { get; private set; } = ConstructionActivityState.Enabled;
    public abstract ConstructionID ConstructionID { get; }

    private ObjectHierarchyMethodsExecutor _hierarchyMethodsExecutor;
    private bool _startWasCalled;

    protected void Awake()
    {
        _hierarchyMethodsExecutor = new ObjectHierarchyMethodsExecutor(this);
        _hierarchyMethodsExecutor.Execute(HierarchyMethodType.On_Awake);
    }

    protected void Start()
    {
        if (ActivityState is ConstructionActivityState.Enabled)
        {
            _startWasCalled = true;
            _hierarchyMethodsExecutor.Execute(HierarchyMethodType.On_Start);
        }
    }

    protected void Update()
    {
        if (ActivityState is ConstructionActivityState.Enabled)
            _hierarchyMethodsExecutor.Execute(HierarchyMethodType.On_Update);
    }

    public void SetActivityState(ConstructionActivityState activityState)
    {
        if (ActivityState == activityState)
            return;

        ActivityState = activityState;

        if (!_startWasCalled)
        {
            _startWasCalled = true; 
            _hierarchyMethodsExecutor.Execute(HierarchyMethodType.On_Start);
        }
    }
}
