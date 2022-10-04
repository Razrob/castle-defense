using UnityEngine;

public abstract class ConstructionBase : MonoBehaviour, IConstruction
{
    public abstract ConstructionType ConstructionType { get; }

    private void Awake()
    {
        OnAwake();
    }

    protected virtual void OnAwake() { }
}
