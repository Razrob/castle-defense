using UnityEngine;

public class DefaultDamageApplicator : IDamageApplicator
{
    public float Damage { get; private set; }
    public Vector3 DamageDirection { get; private set; }

    public DefaultDamageApplicator(float damage, Vector3? damageDirection = null)
    {
        Damage = damage;
        DamageDirection = damageDirection ?? Vector3.zero;
    }
}