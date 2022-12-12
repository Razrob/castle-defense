using System;

public interface IHealth
{
    public float Health { get; }
    public event Action<IHealth> OnHealthEnd;
}