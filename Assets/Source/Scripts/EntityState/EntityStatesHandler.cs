using System;
using static EntityStateConfig;

public class EntityStatesHandler
{
    private EntityState[] _entityState;
    private EntityState _startEntityState;

    public event Action StateOnСhanged;

    public EntityState StartEntityState {
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
        _entityState = entityState.States;
    }
}
