using System;
using UnityEngine;

public struct ProjectileCollisionInfo
{
    public Type ComponentType;
    public Component CollisionComponent;

    public TComponent TryGetComponent<TComponent>()
    {
        if (CollisionComponent.TryCast(out TComponent component))
            return component;

        return default;
    }
}
