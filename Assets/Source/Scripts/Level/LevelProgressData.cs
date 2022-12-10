using System;
using UnityEngine;

[Serializable]
public class LevelProgressData
{
    [SerializeField] private int _completedLevelsCount;

    public int CompletedLevelsCount => _completedLevelsCount;
    public LevelInfo ActiveLevelInfo { get; private set; }
    public bool LevelIsActive => ActiveLevelInfo != null;

    public event Action OnLevelComplete;
    public event Action OnLevelStart;

    public void CompleteLevel()
    {
        _completedLevelsCount++;

        OnLevelComplete?.Invoke();

        ActiveLevelInfo = null;
    }

    public void StartNextLevel(LevelConfig levelConfig)
    {
        if (ActiveLevelInfo != null)
            throw new InvalidOperationException($"");

        ActiveLevelInfo = new LevelInfo(levelConfig);
        OnLevelStart?.Invoke();
    }
}
