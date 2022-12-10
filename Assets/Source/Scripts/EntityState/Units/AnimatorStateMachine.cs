using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AnimatorStateMachine
{
    private AnimatorState _activeState;
    private Animator _animator;
    private readonly Dictionary<AnimatorState, AnimatorStateBase> _states;

    public AnimatorState ActiveState => _activeState;

    public AnimatorStateMachine(Animator animator, IEnumerable<AnimatorStateBase> states, AnimatorState activeState)
    {
        _states = states.ToDictionary(state => state.State, state => state);
        _activeState = activeState;
        _animator = animator;
    }

    public void SetState(AnimatorState state)
    {
        _activeState = state;
        _states[_activeState].SetActive(_animator);
    }

    public void SetState(AnimatorState state, int numberAnimation)
    {
        _activeState = state;
        _states[_activeState].SetActive(_animator, numberAnimation);
    }
}
