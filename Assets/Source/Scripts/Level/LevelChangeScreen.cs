using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelChangeScreen : UIScreen
{
    [SerializeField] private TMP_Text _levelText;
    [SerializeField] private CanvasGroup _labelCanvasGroup;

    private TweenerCore<float, float, FloatOptions> _labelActiveChangeTweener;

    private Dictionary<LevelChangeLabelType, string> _labels;

    public const float LABEL_ACTIVE_CHANGE_DURATION = 2.0f;

    private void Awake()
    {
        _labelCanvasGroup.alpha = 0.0f;

        _labels = new Dictionary<LevelChangeLabelType, string>()
        {
            { LevelChangeLabelType.Complete, "LEVEL" },
            { LevelChangeLabelType.Lose, "YOU LOST" },
        };
    }

    public void SetText(string message, LevelChangeLabelType levelChangeLabelType)
    {
        _levelText.text = $"{_labels[levelChangeLabelType]} {message}";
    }

    public void SetLabelActive(bool value, Action callback = null)
    {
        _labelActiveChangeTweener?.Kill();

        float targetAlfa = Convert.ToSingle(value);
        _labelActiveChangeTweener = 
            DOTween.To(() => _labelCanvasGroup.alpha, value => _labelCanvasGroup.alpha = value, targetAlfa, LABEL_ACTIVE_CHANGE_DURATION)
            .SetEase(Ease.Linear)
            .OnComplete(() => callback?.Invoke());
    }
}
