using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

[DefaultExecutionOrder(-3000)]
public class CycleInitializersParent : MonoBehaviour
{
    private readonly BindingFlags _defaultFlag = BindingFlags.Instance | BindingFlags.Public |  BindingFlags.NonPublic;
    private Dictionary<CycleState, CycleInitializersHandler> _initializerHandlers;
    private object _flowLocker;

    public IReadOnlyDictionary<CycleState, CycleInitializersHandler> CycleInitializersHandlers { get; private set; }

    private void Awake()
    {
        InitInitializers();
    }

    private void InitInitializers()
    {
        CycleInitializersHandlers = GetComponentsInChildren<CycleInitializersHandler>(true)
            .ToDictionary(handler => handler.CycleState, handler => handler);
    }
}
