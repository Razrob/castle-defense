using System;
using System.Collections.Generic;
using System.Linq;

public class EntityStateMachine
{
    private readonly Dictionary<EntityStateID, EntityStateBase> _states;
    public EntityStateID ActiveState { get; private set; }

    public EntityStateMachine(IEnumerable<EntityStateBase> states, EntityStateID activeState)
    {
        _states = states.ToDictionary(state => state.EntityStateID, state => state);
        ActiveState = activeState;
        _states[ActiveState].OnStateEnter();
    }

    public void SetState(EntityStateID entityState)
    {
        if (ActiveState == entityState)
            return;

        if (!_states.ContainsKey(entityState))
            throw new KeyNotFoundException();
       
        _states[ActiveState].OnStateExit();

        ActiveState = entityState;

        _states[ActiveState].OnStateEnter();
    }
 
    public TState GetState<TState>(EntityStateID stateID) where TState : EntityStateBase
    {
        return _states[stateID].Cast<TState>();
    }

    public TState GetState<TState>(Predicate<TState> predicate = null) where TState : EntityStateBase
    {
        foreach (EntityStateBase entityState in _states.Values)
            if (entityState.CastPossible<TState>() && (predicate is null || predicate(entityState.Cast<TState>())))
                return entityState.Cast<TState>();

        return default;
    }

    public void OnUpdate()
    {
        _states[ActiveState].OnUpdate();
    }

    public void OnFixedUpdate()
    {
        _states[ActiveState].OnFixedUpdate();
    }
}
