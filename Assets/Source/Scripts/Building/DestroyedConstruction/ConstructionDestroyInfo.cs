using System;
using UnityEngine;

public struct ConstructionDestroyInfo
{
    public float DestroyDelay;
    public float DestroyDuration;
    public float PartsLiquidateInterval;
    public Vector3 PartsVelocity;
    public Action<Rigidbody> PartForceCalculateAction;
}
