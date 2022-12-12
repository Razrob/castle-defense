using System;
using UnityEngine;

public abstract class ConstructionBase : MonoBehaviour, IConstruction, ITriggerable, IDamagable,
    IPoolable<ConstructionBase, ConstructionID>, IPoolEventListener
{
    public ConstructionLevel ConstructionLevel { get; private set; } = ConstructionLevel.Level_1;
    public ConstructionActivityState ActivityState { get; private set; } = ConstructionActivityState.Enabled;
    public abstract ConstructionID ConstructionID { get; }
    public abstract ConstructionSkinBase ConstructionSkinBase { get; }
    public IHealth Health => _healthStorage;
    public virtual Vector3 OptionalTimerOffcet { get; }
    public bool IsDied { get; private set; }

    protected abstract HealthBarBase _healthBarBase { get; }

    public ConstructionID Identifier => ConstructionID;

    private HealthStorage _healthStorage;
    private ObjectHierarchyMethodsExecutor _hierarchyMethodsExecutor;
    private bool _awakeWasCalled;
    private bool _startWasCalled;

    public event Action<ConstructionBase> OnActivityStateChange;
    public event Action<IDamagable, IDamageApplicator> OnDamageTake;
    public event Action<ConstructionBase> OnConstructionDied;

    public virtual event Action<ConstructionBase> ElementReturnEvent;
    public virtual event Action<ConstructionBase> ElementDestroyEvent;

    private void Awake()
    {
        if (_awakeWasCalled)
            return;

        _awakeWasCalled = true;

        _healthStorage = new HealthStorage(100, 100);
        _hierarchyMethodsExecutor = new ObjectHierarchyMethodsExecutor(this);
        _hierarchyMethodsExecutor.Execute(HierarchyMethodType.On_Awake);
    }

    private void Start()
    {
        Awake();

        if (ActivityState is ConstructionActivityState.Enabled && !_startWasCalled)
        {
            _startWasCalled = true;
            _hierarchyMethodsExecutor.Execute(HierarchyMethodType.On_Start);
        }

        if (_healthBarBase != null)
        {
            _healthStorage.OnStateChange += OnHealthChange;
            _healthStorage.OnHealthEnd += health =>
            {
                if (!IsDied)
                {
                    IsDied = true;
                    OnConstructionDied?.Invoke(this);
                }
            };
        }
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

    protected Projectile PrepareProjectile(ProjectileShape projectileShape, float damage, Vector3 startPosition,
        Action<Projectile> overridenOnCollision = null, ProjectileConstructInfo? overridenConstructInfo = null)
    {
        Projectile projectile = FWC.GlobalData.ProjectilesPool.ExtractElement(projectileShape);
        projectile.SetPosition(startPosition);

        ProjectileConstructInfo constructInfo = new ProjectileConstructInfo
        {
            CollisionMask = overridenConstructInfo?.CollisionMask ?? typeof(IDamagable),
            Damage = overridenConstructInfo?.Damage ?? damage,
            Filter = overridenConstructInfo?.Filter ?? (component => !component.CastPossible<ConstructionBase>()),
        };

        projectile.Construct(constructInfo);

        projectile.OnCollision += overridenOnCollision ??
        (p =>
        {
            p.LastCollisionInfo.Value.TryGetComponent<IDamagable>().TakeDamage(p);
        });

        return projectile;
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

    public void Return()
    {
        ElementReturnEvent?.Invoke(this);
    }

    public void OnElementReturn()
    {
        IsDied = false;
        OnElementReturnCallback();
    }

    public void OnElementExtract()
    {
        OnElementExtractCallback();
    }

    public virtual void OnElementReturnCallback() { }
    public virtual void OnElementExtractCallback() { }
}
