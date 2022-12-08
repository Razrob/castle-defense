using UnityEngine;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;

public abstract class UnitEntityStateBase : EntityStateBase
{
    public readonly Animator Animator;

    private TweenerCore<float, float, FloatOptions> _layerWeightChangeTweener;

    public UnitEntityStateBase(Animator animator)
    {
        Animator = animator;
    }

    protected void ChangeLayerWeight(int layerIndex, float weight)
    {
        _layerWeightChangeTweener?.Kill();
        _layerWeightChangeTweener = DOTween.To(() => Animator.GetLayerWeight(layerIndex),
            value => Animator.SetLayerWeight(layerIndex, value), weight, 0.4f);
    }
}
