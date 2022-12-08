using DG.Tweening;
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
        StartNextLevel();
    }

    private void StartNextLevel()
    {
        int levelIndex = Mathf.Min(_levelsCollection.Levels.Count - 1, FWC.GlobalData.LevelProgressData.CompletedLevelsCount);

        _levelChangeScreen.SetText(levelIndex + 1);
        _levelChangeScreen.SetLabelActive(true);

        Sequence sequence = DOTween.Sequence();
        sequence.AppendInterval(LevelChangeScreen.LABEL_ACTIVE_CHANGE_DURATION + LABEL_VISIBLE_DURATION);
        sequence.AppendCallback(() => _levelChangeScreen.SetLabelActive(false));
        sequence.AppendCallback(() => FWC.GlobalData.LevelProgressData.StartNextLevel(_levelsCollection.Levels[levelIndex]));
    }

    private void OnLevelComplete()
    {
        StartNextLevel();
    }
}
