public class HealthStorage : ResourceStorage, IHealth
{
    public float Health => CurrentValue;

    public HealthStorage(float currentValue, float capacity) : base(currentValue, capacity) { }
}