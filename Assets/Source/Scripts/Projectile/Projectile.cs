using System;
using UnityEngine;

public class Projectile : MonoBehaviour, IPoolable<Projectile, ProjectileShape>, IPoolEventListener, IDamageApplicator
{
    [SerializeField] private Rigidbody _rigidbody;

    public Type CollisionMask { get; private set; }
    public ProjectileCollisionInfo? LastCollisionInfo { get; private set; }
    public Rigidbody Rigidbody => _rigidbody;

    public ProjectileShape Identifier { get; private set; }
    public float Damage { get; private set; }

    public event Action<Projectile> OnCollision;
    public event Action<Projectile> ElementReturnEvent;
    public event Action<Projectile> ElementDestroyEvent;

    public void Construct(Type collisionMask, ProjectileShape projectileShape, float damage)
    {
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
        gameObject.SetActive(false);
        OnCollision = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (CollisionMask is null)
            return;

        if (!other.TryGetComponent(CollisionMask, out Component component))
            return;

        LastCollisionInfo = new ProjectileCollisionInfo
        {
            ComponentType = CollisionMask,
            CollisionComponent = component,
        };

        OnCollision?.Invoke(this);
    }
}
