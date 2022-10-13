using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

[ExecuteAlways]
[DefaultExecutionOrder(500)]
public class CycleFrameworkBootloader : MonoBehaviour
{
    [SerializeField] private CycleState _startState;

    private CycleInitializersParent _initializersParent;
    private CycleStateMachine _cycleStateMachine;
    private CycleEventsRepository _cycleEventsRepository;
    private CycleEventsProcessor _cycleEventsProcessor;

    private void OnEnable() => TryInitHierar�hy();
    private void OnTransformChildrenChanged() => TryInitHierar�hy();

    private void Awake()
    {
        TryInitHierar�hy();

        if (Application.isPlaying)
            InitFramework();
    }

    private void TryInitHierar�hy()
    {
        if (_initializersParent is null)
        {
            _initializersParent = GetComponentInChildren<CycleInitializersParent>(true);

            if (_initializersParent is null)
            {
                _initializersParent = new GameObject("Initializers").AddComponent<CycleInitializersParent>();
                _initializersParent.transform.parent = transform;
            }
        }
    }

    private void InitFramework()
    {
        _cycleStateMachine = new CycleStateMachine(_startState);
        Dictionary<CycleMethodType, MethodInfo> methods = new Dictionary<CycleMethodType, MethodInfo>();

        foreach (CycleMethodType cycleMethodType in (IEnumerable<CycleMethodType>)Enum.GetValues(typeof(CycleMethodType)))
            methods.Add(cycleMethodType, CycleEventsExtractor.ExtractSpecificEventInfo(cycleMethodType));

        _cycleEventsRepository = new CycleEventsRepository(methods, _initializersParent.CycleInitializersHandlers.Values);
        _cycleEventsProcessor = new CycleEventsProcessor(_cycleEventsRepository);

        CycleEventsTransmitter cycleEventsTransmitter = 
            new GameObject($"[Framework] {nameof(CycleEventsTransmitter)}").AddComponent<CycleEventsTransmitter>();

        cycleEventsTransmitter.transform.parent = transform;
        cycleEventsTransmitter.transform.SetSiblingIndex(0);

        cycleEventsTransmitter.Init(_cycleStateMachine, _cycleEventsProcessor);

        new FWC(_cycleStateMachine);
    }
}
