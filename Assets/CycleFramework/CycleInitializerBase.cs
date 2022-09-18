﻿using UnityEngine;

public abstract class CycleInitializerBase : MonoBehaviour
{
    protected virtual void OnInit() { }
    protected virtual void OnUpdate() { }
    protected virtual void OnFixedUpdate() { }
}
