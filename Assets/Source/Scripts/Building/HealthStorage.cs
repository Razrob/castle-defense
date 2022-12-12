using System;

public class HealthStorage : ResourceStorage, IHealth
{
    public float Health => CurrentValue;
    public event Action<IHealth> OnHealthEnd;

    public HealthStorage(float currentValue, float capacity) : base(currentValue, capacity) 
    {
        OnResourceEnd += () => OnHealthEnd?.Invoke(this);
    }
}