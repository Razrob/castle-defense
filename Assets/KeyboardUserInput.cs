using System;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardUserInput : MonoBehaviour
{
    private static Dictionary<KeyCode, Action<bool,KeyCode>> _registeredEvents;
    private static KeyboardUserInput _instance;

    [RuntimeInitializeOnLoadMethod]
    private static void Initalize()
    {
        _registeredEvents = new Dictionary<KeyCode, Action<bool,KeyCode>>();

        _instance = FindObjectOfType<KeyboardUserInput>(true);

        if (_instance is null)
            _instance = new GameObject("KeyboardUserInput").AddComponent<KeyboardUserInput>();
    }

    private void Update()
    {
        foreach (KeyCode key in _registeredEvents.Keys)
        {
            if (Input.GetKeyUp(key))
                _registeredEvents[key]?.Invoke(false,key);
            if (Input.GetKeyDown(key))
                _registeredEvents[key]?.Invoke(true,key);
        }
    }

    public static void RegisterCallback(KeyCode keyCode, Action<bool, KeyCode> callback)
    {
        if (!_registeredEvents.ContainsKey(keyCode))
            _registeredEvents.Add(keyCode, null);

        _registeredEvents[keyCode] += callback;
    }
}