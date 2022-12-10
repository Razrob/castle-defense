using DG.Tweening;
using System;
using UnityEngine;
using Zenject.SpaceFighter;

public class Projectile : MonoBehaviour, IPoolable<Projectile, ProjectileShape>, IPoolEventListener, IDamageApplicator
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private ParticleSystem _explosion;

    private Predicate<Component> _filter;

    public Type CollisionMask { get; private set; }
    public ProjectileCollisionInfo? LastCollisionInfo { get; private set; }
    public Rigidbody Rigidbody => _rigidbody;

    public ProjectileShape Identifier { get; private set; }
    public float Damage { get; private set; }

    public Vector3 DamageDirection { get; private set; }

    public event Action<Projectile> OnCollision;
    public event Action<Projectile> ElementReturnEvent;
    public event Action<Projectile> ElementDestroyEvent;

    public void Construct(Type collisionMask, ProjectileShape projectileShape, float damage, Predicate<Component> filter = null)
    {
        _filter = filter;
        CollisionMask = collisionMask;
        Identifier = projectileShape;
        Damage = damage;
    }

    public void OnElementExtract()
    {
        gameObject.SetActive(true);
    }

    public void OnElementReturn()
    {
        Identifier = 0;
        CollisionMask = null;
        LastCollisionInfo = null;
        OnCollision = null;

        if (_explosion)
            _explosion.Stop();

        DamageDirection = Vector3.zero;

        gameObject.SetActive(false);
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
    }
}
