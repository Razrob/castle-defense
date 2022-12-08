using System;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-100)]
public class UIScreenRepository : MonoBehaviour
{
    private Dictionary<Type, UIScreen> _screens;

    private static UIScreenRepository _instance;

    public static UIScreenRepository Instance
    {
        get
        {
            if (_instance is null)
                Init();

            return _instance;
        }
    }

    public static IReadOnlyDictionary<Type, UIScreen> Screens => Instance._screens;

    private static void Init()
    {
        if (_instance != null)
            return;

        _instance = FindObjectOfType<UIScreenRepository>();
        Instance._screens = new Dictionary<Type, UIScreen>();

        foreach (UIScreen screen in FindObjectsOfType<UIScreen>(true))
            Instance._screens.Add(screen.GetType(), screen);
    }

    public static TScreen GetScreen<TScreen>() where TScreen : UIScreen
    {
        if (Instance is null)
            throw new NullReferenceException($"Instance is null");

        if (Instance._screens.TryGetValue(typeof(TScreen), out UIScreen screen))
            return (TScreen)screen;

        return default;
    }
}
