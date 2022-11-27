using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using System;
using UnityEngine;

public abstract class SpriteTimerBase : MonoBehaviour, IPoolable<SpriteTimerBase, TimerType>, IPoolEventListener
{
    [SerializeField] private AdvancedSprite _sprite;
    [SerializeField] private Transform _dynamicArrow;

    private readonly Color _startFillSpriteColor = new Color(255 / 255f, 82 / 255f, 90 / 255f, 172 / 255f);
    private readonly Color _finalFillSpriteColor = new Color(122 / 255f, 255 / 255f, 81 / 255f, 172 / 255f);

    private TweenerCore<float, float, FloatOptions> _timerTweener;

    protected virtual bool _isPoolable => true;

    public TimerType Identifier => TimerType;
    public abstract TimerType TimerType { get; }
    public bool TimerInProcess { get; private set; }

    public event Action<SpriteTimerBase> OnComplete;
    public event Action<SpriteTimerBase> ElementReturnEvent;
    public event Action<SpriteTimerBase> ElementDestroyEvent;

    private void Awake()
    {
        _sprite.FillType = FillType.Radial;
        _sprite.Inverse = false;
    }

    private void SetTimerActive(bool value)
    {
        gameObject.SetActive(value);
    }

    protected void StartTimer(float duration)
    {
        SetTimerActive(true);

        _sprite.AttachedSpriteRenderer.color = _startFillSpriteColor;
        TimerInProcess = true;
        _sprite.Fill = 0f;

        _timerTweener?.Kill();
        _timerTweener = DOTween.To(() => 0f, 
            value => 
            { 
                _sprite.AttachedSpriteRenderer.color = Color.Lerp(_startFillSpriteColor, _finalFillSpriteColor, value);
                _sprite.Fill = value;

                float arrowAngle = 360f * value;
                _dynamicArrow.transform.eulerAngles = _dynamicArrow.transform.eulerAngles.SetZ(-arrowAngle);
            }, 
            1f, duration)
            .SetEase(Ease.Linear)
            .OnComplete(StopTimer);

        _sprite.transform.parent.DOKill(true);
        _sprite.transform.parent.DOPunchScale(Vector3.one * 0.35f, Mathf.Min(Mathf.Max(1f, duration / 2f), duration), 6);
    }

    public void StopTimer()
    {
        SetTimerActive(false);

        if (!TimerInProcess)
            return;

        _timerTweener?.Kill();
        _sprite.transform.parent.DOKill(true);

        TimerInProcess = false;

        OnComplete?.Invoke(this);

        if (_isPoolable)
            ElementReturnEvent?.Invoke(this);
    }

    public void OnElementReturn()
    {
        OnComplete = null;
        OnElementReturnCallback();
    }

    protected virtual void OnElementReturnCallback() { }

    public void OnElementExtract() { }
}
