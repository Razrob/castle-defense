using DG.Tweening;
using System;
using UnityEngine;
using Zenject;

public class LevelChanger : CycleInitializerBase
{
    [Inject] private readonly GameLevelsCollectionConfig _levelsCollection;
    [Inject] private readonly LevelChangeScreen _levelChangeScreen;

    private const float LABEL_VISIBLE_DURATION = 2.0f;

    protected override void OnInit()
    {
        FWC.GlobalData.LevelProgressData.OnLevelComplete += OnLevelComplete;
        FWC.GlobalData.LevelProgressData.OnLevelLose += OnLevelLose;
        StartNextLevel();
    }

    private void OnLevelComplete()
    {
        StartNextLevel();
    }

    private void OnLevelLose()
    {
        FWC.GlobalData.LevelProgressData.ActiveLevelInfo.IterationUnitsProcessor.RemoveAllUnits();
        _levelChangeScreen.SetText($"", LevelChangeLabelType.Lose);
        _levelChangeScreen.SetLabelActive(true);

        Sequence sequence = DOTween.Sequence();
        sequence.AppendInterval(LevelChangeScreen.LABEL_ACTIVE_CHANGE_DURATION + LABEL_VISIBLE_DURATION);
        sequence.AppendCallback(() => _levelChangeScreen.SetLabelActive(false));
        sequence.AppendInterval(LevelChangeScreen.LABEL_ACTIVE_CHANGE_DURATION);
        sequence.AppendCallback(() => StartNextLevel());
    }

    private void StartNextLevel()
    {
        int levelIndex = Mathf.Min(_levelsCollection.Levels.Count - 1, FWC.GlobalData.LevelProgressData.CompletedLevelsCount);

        _levelChangeScreen.SetText($"{levelIndex + 1}", LevelChangeLabelType.Complete);
        _levelChangeScreen.SetLabelActive(true);

        Sequence sequence = DOTween.Sequence();
        sequence.AppendInterval(LevelChangeScreen.LABEL_ACTIVE_CHANGE_DURATION + LABEL_VISIBLE_DURATION);
        sequence.AppendCallback(() => _levelChangeScreen.SetLabelActive(false));
        sequence.AppendCallback(() => FWC.GlobalData.LevelProgressData.StartNextLevel(_levelsCollection.Levels[levelIndex]));
        sequence.AppendInterval(LevelChangeScreen.LABEL_ACTIVE_CHANGE_DURATION);

        sequence.AppendCallback(() =>
        {
            if (FWC.GlobalData.LevelProgressData.ActiveLevelInfo.IterationUnitsProcessor.AllUnitsOnLevelDied())
            {
                FWC.GlobalData.LevelProgressData.CompleteLevel();
            }
            else
            {
                FWC.GlobalData.LevelProgressData.ActiveLevelInfo.IterationUnitsProcessor
                    .OnAllUnitsOnLevelDied += () => FWC.GlobalData.LevelProgressData.CompleteLevel();
            }
        });
    }
}
