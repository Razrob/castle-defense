using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class HealthBarBase : MonoBehaviour
{
    [SerializeField] protected AdvancedSprite _fillSprite;

    private IReadOnlyList<SpriteRenderer> _childSpriteRenderers;

    private const float BAR_VISIBLE_DURATION = 4.0f;
    private const float BAR_VISIBLE_CHANGE_DURATION = 0.4f;
    private float _barAlfa;

    private Sequence _visibleChangeSequence;

    private void Awake()
    {
        _childSpriteRenderers = GetComponentsInChildren<SpriteRenderer>(true);
        SetAlfaInternal(0f);
    }

    private void SetAlfaInternal(float alfa)
    {
        _barAlfa = alfa;

        foreach (SpriteRenderer spriteRenderer in _childSpriteRenderers)
            spriteRenderer.color = spriteRenderer.color.SetAlfa(alfa);
    }

    public virtual void SetFill(float value, bool changeAlfa = true)
    {
        _fillSprite.Fill = value;

        if (!changeAlfa)
            return;

        _visibleChangeSequence?.Kill();

        if (value < 0.01f)
        {
            SetAlfaInternal(0f);
            return;
        }

        _visibleChangeSequence = DOTween.Sequence();

        _visibleChangeSequence.Append(DOTween.To(() => _barAlfa, alfa => SetAlfaInternal(alfa), 1f, BAR_VISIBLE_CHANGE_DURATION));
        _visibleChangeSequence.AppendInterval(BAR_VISIBLE_DURATION);
        _visibleChangeSequence.Append(DOTween.To(() => _barAlfa, alfa => SetAlfaInternal(alfa), 0f, BAR_VISIBLE_CHANGE_DURATION));
    }

    public virtual void SetVisibleForced(bool value)
    {
        _visibleChangeSequence?.Kill();
        _visibleChangeSequence = DOTween.Sequence();

        _visibleChangeSequence
            .Append(DOTween.To(() => _barAlfa, alfa => SetAlfaInternal(alfa), Convert.ToSingle(value), BAR_VISIBLE_CHANGE_DURATION));

        if (value)
        {
            _visibleChangeSequence.AppendInterval(BAR_VISIBLE_DURATION);
            _visibleChangeSequence.Append(DOTween.To(() => _barAlfa, alfa => SetAlfaInternal(alfa), 0f, BAR_VISIBLE_CHANGE_DURATION));
        }
    }
}
