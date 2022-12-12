using System;
using UnityEngine;

public class ConstructionTriggerBehaviour : TriggerZone
{
    [SerializeField] private SphereCollider _sphereCollider;

    protected override Func<ITriggerable, bool> EnteredComponentIsSuitable => component => component.CastPossible<ConstructionBase>();

    public void SetTriggerRadius(float value)
    {
        _sphereCollider.radius = value;
    }
}
