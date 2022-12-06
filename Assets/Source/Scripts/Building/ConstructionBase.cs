using System;
using UnityEngine;

public abstract class ConstructionBase : MonoBehaviour, IConstruction, ITriggerable, IDamagable
{
    public ConstructionLevel ConstructionLevel { get; private set; } = ConstructionLevel.Level_1;
    public ConstructionActivityState ActivityState { get; private set; } = ConstructionActivityState.Enabled;
    public abstract ConstructionID ConstructionID { get; }
    public abstract ConstructionSkinBase ConstructionSkinBase { get; }
    public IHealth Health => _healthStorage;

    protected abstract HealthBarBase _healthBarBase { get; }

    private HealthStorage _healthStorage;
    private ObjectHierarchyMethodsExecutor _hierarchyMethodsExecutor;
    private bool _startWasCalled;

    public event Action<ConstructionBase> OnActivityStateChange;
    public event Action<IDamagable, IDamageApplicator> OnDamageTake;

    private void Awake()
    {
        _healthStorage = new HealthStorage(100, 100);
        _hierarchyMethodsExecutor = new ObjectHierarchyMethodsExecutor(this);
        _hierarchyMethodsExecutor.Execute(HierarchyMethodType.On_Awake);
    }

    private void Start()
    {
        if (ActivityState is ConstructionActivityState.Enabled && !_startWasCalled)
        {
            _startWasCalled = true;
            _hierarchyMethodsExecutor.Execute(HierarchyMethodType.On_Start);
        }

        _healthStorage.OnStateChange += OnHealthChange;
        OnHealthChange();
    }

    private void Update()
    {
        if (ActivityState is ConstructionActivityState.Enabled)
            _hierarchyMethodsExecutor.Execute(HierarchyMethodType.On_Update);
    }

    private void OnHealthChange()
    {
        _healthBarBase?.SetFill(_healthStorage.CurrentValue / _healthStorage.Capacity);
    }

    public void SetActivityState(ConstructionActivityState activityState)
    {
        if (ActivityState == activityState)
            return;

        ActivityState = activityState;

        Start();

        OnActivityStateChange?.Invoke(this);
    }

    public void TakeDamage(IDamageApplicator damageApplicator)
    {
        _healthStorage.ChangeValue(-damageApplicator.Damage);
        OnDamageTake?.Invoke(this, damageApplicator);
    }

    public void Upgrade()
    {

    }
}
