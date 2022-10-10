using System;
using System.Collections.Generic;
using System.Linq;

public class EntityStatesHandler
{
    private EntityStateID[] _entityState;
    private EntityStateID _startEntityState;

    public event Action StateOnСhanged;

    public EntityStateID StartEntityState {
        get
        {
            return _startEntityState;
        }
        set
        {
            StateOnСhanged?.Invoke();
            _startEntityState = value;
        }
   
    }
  

    public EntityStatesHandler(EntityStateConfig entityState)
    {
        _entityState = entityState.States.ToArray();
    }
}


public class EntityStateMachine
{
    private readonly Dictionary<EntityStateID, EntityStateBase> _states;
    public EntityStateID ActiveState { get; private set; }

    public EntityStateMachine(IEnumerable<EntityStateBase> states, EntityStateID activeState)
    {
        _states = states.ToDictionary(state => state.EntityStateID, state => state);
        ActiveState = activeState;
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

    public void OnUpdate()
    {
        _states[ActiveState].OnUpdate();
    }
}

public abstract class EntityStateBase
{
    public abstract EntityStateID EntityStateID { get; }
    public abstract void OnStateEnter();
    public abstract void OnStateExit();
    public abstract void OnUpdate();
}