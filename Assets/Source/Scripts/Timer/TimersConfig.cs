using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "TimersConfig", menuName = "Config/TimersConfig")]
public class TimersConfig : ScriptableObject, ISingleConfig
{
    [SerializeField] private SpriteTimerBase[] _timersPrefabs;

    private Dictionary<Type, SpriteTimerBase> _prefabsTypes;
    private Dictionary<TimerType, SpriteTimerBase> _prefabs;

    private void OnEnable()
    {
        _prefabsTypes = _timersPrefabs?.ToDictionary(p => p.GetType(), p => p);
        _prefabs = _timersPrefabs?.ToDictionary(p => p.TimerType, p => p);
    }

    public TTimer GetTimer<TTimer>() where TTimer : SpriteTimerBase
    {
        if (!_prefabsTypes.ContainsKey(typeof(TTimer)))
            return default;

        return _prefabsTypes[typeof(TTimer)].Cast<TTimer>();
    }

    public TTimer GetTimer<TTimer>(TimerType timerType) where TTimer : SpriteTimerBase
    {
        if (!_prefabs.ContainsKey(timerType))
            return default;

        return _prefabs[timerType].Cast<TTimer>();
    }
}