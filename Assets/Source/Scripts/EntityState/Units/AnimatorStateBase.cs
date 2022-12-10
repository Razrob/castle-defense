using UnityEngine;

public class AnimatorStateBase
{
    private AnimatorState _state;
    private int _countAnimations;
    private int _layer;

    public int CountAnimations => _countAnimations;
    public AnimatorState State => _state;

    public AnimatorStateBase(AnimatorState state, int countAnimations, int layer)
    {
        _state = state;
        _countAnimations = countAnimations;
        _layer = layer;
    }

    public void SetActive(Animator animator)
    {
        animator.SetLayerWeight(_layer, 1);
    }

    public void SetActive(Animator animator, int numberAnimation)
    {
        animator.SetFloat("AttackType", numberAnimation);
        animator.SetLayerWeight(_layer, 1);
    }
}
