using System;
using UnityEngine;
using UnityEngine.AI;

public abstract class UnitBase : MonoBehaviour, IUnit, ITriggerable, IDamagable
{
    [SerializeField] private Animator _animator;
    [SerializeField] private NavMeshAgent _navMeshAgent;

    private ObjectHierarchyMethodsExecutor _hierarchyMethodsExecutor;

    protected ResourceStorage _healthStorage = new ResourceStorage(100, 100);
    protected EntityStateMachine _stateMachine;

    public NavMeshAgent NavMeshAgent => _navMeshAgent;
    public Animator Animator => _animator;
    public EntityStateMachine StateMachine => _stateMachine;
    public Vector3 Velocity => _navMeshAgent.velocity;

    public bool IsDied => _healthStorage.CurrentValue < 1f;

    public abstract UnitType UnitType { get; }

    public IHealth Health => throw new NotImplementedException();

    public event Action<UnitBase> OnUnitDied;
    public event Action<IDamagable, IDamageApplicator> OnDamageTake;

    private void Awake()
    {
        _hierarchyMethodsExecutor = new ObjectHierarchyMethodsExecutor(this);
        _hierarchyMethodsExecutor.Execute(HierarchyMethodType.On_Awake);
    }
    private void Start()
    {
        _hierarchyMethodsExecutor.Execute(HierarchyMethodType.On_Start);
    }

    private void Update()
    {
        _hierarchyMethodsExecutor.Execute(HierarchyMethodType.On_Update);
    }
    private void FixedUpdate()
    {
        _hierarchyMethodsExecutor.Execute(HierarchyMethodType.On_FixedUpdate);
    }

    protected virtual void OnDamaged() { }

    public void TakeDamage(IDamageApplicator damageApplicator)
    {
        if (IsDied)
            return;

        _healthStorage.ChangeValue(-damageApplicator.Damage);
        OnDamaged();

        if (IsDied)
            OnUnitDied?.Invoke(this);
    }

    public void SetDestination(Vector3 position)
    {
        _navMeshAgent.SetDestination(position);
    }

    public void Warp(Vector3 position)
    {
        _navMeshAgent.Warp(position);
    }

    public bool HasPath()
    {
        return _navMeshAgent.hasPath;
    }
}
