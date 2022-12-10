using UnityEngine;

public interface IDamageApplicator
{
    public float Damage { get; }
    public Vector3 DamageDirection { get; }
}
