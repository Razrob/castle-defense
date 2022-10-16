using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

[DefaultExecutionOrder(-1000)]
public class CycleFrameworkBootloader : MonoBehaviour
{
    [SerializeField] private CycleState _startState;

    private CycleInitializersParent _initializersParent;
    private CycleStateMachine _cycleStateMachine;
    private CycleEventsRepository _cycleEventsRepository;
    private CycleEventsProcessor _cycleEventsProcessor;

    private void Awake()
    {
        InitFramework();
    }

    private void Start()
    {
        CycleEventsTransmitter cycleEventsTransmitter =
            new GameObject($"[Framework] {nameof(CycleEventsTransmitter)}").AddComponent<CycleEventsTransmitter>();

        cycleEventsTransmitter.transform.parent = transform;
        cycleEventsTransmitter.transform.SetSiblingIndex(0);

        cycleEventsTransmitter.Init(_cycleStateMachine, _cycleEventsProcessor);
    }

    private void InitFramework()
    {
        _initializersParent = GetComponentInChildren<CycleInitializersParent>(true);
        _cycleStateMachine = new CycleStateMachine(_startState);
        Dictionary<CycleMethodType, MethodInfo> methods = new Dictionary<CycleMethodType, MethodInfo>();

        foreach (CycleMethodType cycleMethodType in (IEnumerable<CycleMethodType>)Enum.GetValues(typeof(CycleMethodType)))
            methods.Add(cycleMethodType, CycleEventsExtractor.ExtractSpecificEventInfo(cycleMethodType));

        _cycleEventsRepository = new CycleEventsRepository(methods, _initializersParent.CycleInitializersHandlers.Values);
        _cycleEventsProcessor = new CycleEventsProcessor(_cycleEventsRepository);

        new FWC(_cycleStateMachine);
    }
}
