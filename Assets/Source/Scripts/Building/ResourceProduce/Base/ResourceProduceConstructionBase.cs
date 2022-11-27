using System;

public abstract class ResourceProduceConstructionBase : ConstructionBase
{
    public abstract ResourceProduceCoreBase ResourceProduceCoreBase { get; }
    public ResourceProduceState ResourceProduceState { get; private set; } = ResourceProduceState.Completed;

    public event Action<ResourceProduceConstructionBase> OnProduceStateChange;

    public void SetProduceState(ResourceProduceState state)
    {
        if (ResourceProduceState == state)
            return;

        ResourceProduceState = state;
        OnProduceStateChange?.Invoke(this);
    }
}