using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "ConfigsRepository", menuName = "Config/ConfigsRepository")]
public class ConfigsRepository : ScriptableObject
{
    [SerializeField] private ScriptableObject[] _configs;

    private static ConfigsRepository _instance;
    private IReadOnlyDictionary<Type, ScriptableObject> _configsDictionary;

    private void OnEnable()
    {
        _instance = this;
        Refresh();
    }

    private void Refresh()
    {
        _configsDictionary = _configs?.ToDictionary(config => config.GetType(), config => config);
    }

    public static TConfig FindConfig<TConfig>() where TConfig : ScriptableObject
    {
        if (_instance._configsDictionary is null || _instance._configs.Length != _instance._configsDictionary.Count)
            _instance.Refresh();

        if (_instance._configsDictionary.TryGetValue(typeof(TConfig), out ScriptableObject config))
            return (TConfig)config;

        return default;
    }
}
