using DG.Tweening;
using System;
using UnityEngine;

public struct ProjectileConstructInfo
{
    public Type CollisionMask;
    public float Damage;
    public Predicate<Component> Filter;
}

public class Projectile : MonoBehaviour, IPoolable<Projectile, ProjectileShape>, IPoolEventListener, IDamageApplicator
{
    [SerializeField] private ProjectileShape _projectileShape;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private ParticleSystem _explosion;
    [SerializeField] private ParticleSystem _constructEffect;
    [SerializeField] private Vector3 _size;

    private Predicate<Component> _filter;

    public Type CollisionMask { get; private set; }
    public ProjectileCollisionInfo? LastCollisionInfo { get; private set; }
    public Rigidbody Rigidbody => _rigidbody;
    public Vector3 Size => _size;

    public ProjectileShape Identifier => _projectileShape;
    public float Damage { get; private set; }

    public Vector3 DamageDirection { get; private set; }

    public event Action<Projectile> OnCollision;
    public event Action<Projectile> ElementReturnEvent;
    public event Action<Projectile> ElementDestroyEvent;

    public void Construct(ProjectileConstructInfo constructInfo)
    {
        _filter = constructInfo.Filter;
        CollisionMask = constructInfo.CollisionMask;
        Damage = constructInfo.Damage;

        if (_constructEffect)
            _constructEffect.Play();
    }

    private void Update()
    {
        if (_rigidbody.velocity.magnitude > 0.1f && CollisionMask != null)
            transform.rotation = Quaternion.LookRotation(_rigidbody.velocity);
    }

    public void OnElementExtract()
    {
        gameObject.SetActive(true);
    }

    public void OnElementReturn()
    {
        CollisionMask = null;
        LastCollisionInfo = null;
        OnCollision = null;

        if (_explosion)
            _explosion.Stop();

        DamageDirection = Vector3.zero;

        gameObject.SetActive(false);
    }

    public void SetPosition(Vector3 position)
    {
        _rigidbody.position = position;
        transform.position = position;
    }

    public void SetRotation(Quaternion rotation)
    {
        _rigidbody.rotation = rotation;
        transform.rotation = rotation;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (CollisionMask is null)
            return;

        if (!other.TryGetComponent(CollisionMask, out Component component))
            return;

        if (_filter != null && !_filter(component))
            return;

        DamageDirection = _rigidbody.velocity.normalized;

        LastCollisionInfo = new ProjectileCollisionInfo
        {
            ComponentType = CollisionMask,
            CollisionComponent = component,
        };

        if (_explosion)
        {
            _explosion.Play();
            DOTween.Sequence().AppendInterval(_explosion.main.startLifetime.constant).AppendCallback(() => ElementReturnEvent?.Invoke(this));
        }

        OnCollision?.Invoke(this);

        if (!_explosion)
            ElementReturnEvent?.Invoke(this);
    }
}
