using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using System;

public abstract class AttackUnitsAttackStateBase : UnitEntityStateBase
{
    private TweenerCore<float, float, FloatOptions> _lastCycleAttackDelayTweener;
    private TweenerCore<float, float, FloatOptions> _lastCycleAttackTweener;

    public abstract event Action OnUnitAttack;

    public AttackUnitsAttackStateBase(AttackUnit attackUnit) : base(attackUnit.Animator) { }

    protected void RegisterCycleAttackCallback(float delay, float interval, Action callback)
    {
        _lastCycleAttackDelayTweener?.Kill();
        _lastCycleAttackTweener?.Kill();

        Action startTweenerAction = () =>
        {
            _lastCycleAttackTweener = DOTween.To(() => 0f, value => { }, 1f, interval)
                .SetLoops(-1)
                .OnStepComplete(() => callback());
        };

        _lastCycleAttackDelayTweener = DOTween.To(() => 0f, value => { }, 1f, delay)
            .OnComplete(() =>
            {
                callback();
                startTweenerAction();
            });
    }

    public sealed override void OnStateEnter()
    {

        OnStateEnterCallback();
    }

    public sealed override void OnStateExit()
    {
        _lastCycleAttackDelayTweener?.Kill();
        _lastCycleAttackTweener?.Kill();
        OnStateExitCallback();
    }

    public virtual void OnStateEnterCallback() { }
    public virtual void OnStateExitCallback() { }
}
