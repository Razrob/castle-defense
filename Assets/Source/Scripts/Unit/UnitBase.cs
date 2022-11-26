using System;
using UnityEngine;
using UnityEngine.AI;

public abstract class UnitBase : MonoBehaviour, IUnit, ITriggerable, IDamagable
{
    [SerializeField] private Animator _animator;
    [SerializeField] private NavMeshAgent _navMeshAgent;

    protected ResourceStorage _healthStorage = new ResourceStorage(100, 100);
    protected EntityStateMachine _stateMachine;
    protected event Action _updateEvent;
    protected event Action _fixedUpdateEvent;
    protected event Action _startEvent;

    public NavMeshAgent NavMeshAgent => _navMeshAgent;
    public Animator Animator => _animator;
    public EntityStateMachine StateMachine => _stateMachine;
    public Vector3 Velocity => _navMeshAgent.velocity;

    public bool IsDied => _healthStorage.CurrentValue < 1f;

    public abstract UnitType UnitType { get; }

    public IHealth Health => throw new NotImplementedException();

    public event Action<UnitBase> OnUnitDied;
    public event Action<IDamagable, IDamageApplicator> OnDamageTake;

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

    protected virtual void OnDamaged() { }

    protected abstract void Awake();
    protected void Start() => _startEvent?.Invoke();
    protected void Update() => _updateEvent?.Invoke();
    protected void FixedUpdate() => _fixedUpdateEvent?.Invoke();
}
