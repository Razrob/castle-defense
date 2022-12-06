﻿using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using System;
using TMPro;
using UnityEngine;

public class LevelChangeScreen : UIScreen
{
    [SerializeField] private TMP_Text _levelText;
    [SerializeField] private CanvasGroup _labelCanvasGroup;

    private TweenerCore<float, float, FloatOptions> _labelActiveChangeTweener;

    public const float LABEL_ACTIVE_CHANGE_DURATION = 2.0f;

    private void Awake()
    {
        _labelCanvasGroup.alpha = 0.0f;
    }

    public void SetText(int levelNumber)
    {
        _levelText.text = $"LEVEL {levelNumber}";
    }

    public void SetLabelActive(bool value, Action callback = null)
    {
        _labelActiveChangeTweener?.Kill();

        float targetAlfa = Convert.ToSingle(value);
        _labelActiveChangeTweener = 
            DOTween.To(() => _labelCanvasGroup.alpha, value => _labelCanvasGroup.alpha = value, LABEL_ACTIVE_CHANGE_DURATION, targetAlfa)
            .SetEase(Ease.Linear)
            .OnComplete(() => callback?.Invoke());
    }
}