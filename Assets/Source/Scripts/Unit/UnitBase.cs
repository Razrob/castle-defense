using System;
using UnityEngine;
using UnityEngine.AI;

public abstract class UnitBase : MonoBehaviour, IUnit, ITriggerable, IDamagable, IPoolable<UnitBase, UnitID>, IPoolEventListener
{
    [SerializeField] private Collider[] _colliders;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private Animator _animator;
    [SerializeField] private NavMeshAgent _navMeshAgent;
    [SerializeField] private DefaultHealthBar _healthBar;

    private ObjectHierarchyMethodsExecutor _hierarchyMethodsExecutor;

    protected HealthStorage _healthStorage;
    protected EntityStateMachine _stateMachine;

    public NavMeshAgent NavMeshAgent => _navMeshAgent;
    public Animator Animator => _animator;
    public EntityStateMachine StateMachine => _stateMachine;
    public Vector3 Velocity => _navMeshAgent.velocity;

    public bool IsDied => _healthStorage.CurrentValue < 1f;

    public abstract UnitType UnitType { get; }
    public abstract UnitID UnitID { get; }

    public IHealth Health => throw new NotImplementedException();
    public UnitID Identifier => UnitID;
    public IDamageApplicator LastDamageApplicator { get; private set; }

    public event Action<UnitBase, IDamageApplicator> OnUnitDied;
    public event Action<IDamagable, IDamageApplicator> OnDamageTake;

    public event Action<UnitBase> ElementReturnEvent;
    public event Action<UnitBase> ElementDestroyEvent;

    private void Awake()
    {
        _hierarchyMethodsExecutor = new ObjectHierarchyMethodsExecutor(this);
        _hierarchyMethodsExecutor.Execute(HierarchyMethodType.On_Awake);
        Init();
    }

    private void Start()
    {
        _hierarchyMethodsExecutor.Execute(HierarchyMethodType.On_Start);
    }

    private void Init()
    {
        foreach (Collider collider in _colliders)
            collider.enabled = true;
        
        _navMeshAgent.enabled = true;

        _healthStorage = new HealthStorage(100, 100);
        _healthStorage.OnStateChange += () => _healthBar.SetFill(_healthStorage.CurrentValue / _healthStorage.Capacity);

        gameObject.SetActive(true);
    }

    private void Update()
    {
        if (IsDied || !gameObject.activeSelf)
            return;

        _hierarchyMethodsExecutor.Execute(HierarchyMethodType.On_Update);
    }

    private void FixedUpdate()
    {
        if (IsDied || !gameObject.activeSelf)
            return;

        _hierarchyMethodsExecutor.Execute(HierarchyMethodType.On_FixedUpdate);
    }

    private void OnDied(IDamageApplicator damageApplicator)
    {
        foreach (Collider collider in _colliders)
            collider.enabled = false;

        _navMeshAgent.enabled = false;
    }

    private void OnDamaged(IDamageApplicator damageApplicator)
    {
        OnDamagedCallback(damageApplicator);
    }
    protected virtual void OnDamagedCallback(IDamageApplicator damageApplicator) { }

    public void TakeDamage(IDamageApplicator damageApplicator)
    {
        if (IsDied)
            return;

        LastDamageApplicator = damageApplicator;
        _healthStorage.ChangeValue(-damageApplicator.Damage);
        OnDamaged(damageApplicator);

        if (IsDied)
        {
            OnDied(damageApplicator);
            OnUnitDied?.Invoke(this, damageApplicator);
        }
    }

    public void SetDestination(Vector3 position)
    {
        _navMeshAgent.SetDestination(position);
    }

    public void Warp(Vector3 position)
    {
        _navMeshAgent.Warp(position);
    }

    public void SetRotation(Quaternion rotation)
    {
        transform.rotation = rotation;
        _rigidbody.rotation = rotation;
    }

    public bool HasPath()
    {
        return _navMeshAgent.hasPath;
    }

    public void Return() => ElementReturnEvent?.Invoke(this);

    public void OnElementReturn()
    {
        LastDamageApplicator = null;
        gameObject.SetActive(false);
    }

    public void OnElementExtract()
    {
        Init();
    }
}
