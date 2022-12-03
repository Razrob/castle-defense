using UnityEngine;

public abstract class UnitFactoryBehaviourBase : MonoBehaviour
{
    protected void Awake()
    {
        OnInit();
    }

    protected virtual void OnInit() { }

    public abstract UnitType UnitType { get; }

    public abstract UnitConfiguration<IUnit> GetConfiguration(UnitID unitID);
    public abstract TUnit CreateSolid<TUnit>(UnitID unitID)
        where TUnit : IUnit;
}
