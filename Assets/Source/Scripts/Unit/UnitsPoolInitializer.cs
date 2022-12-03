using Zenject;

public class UnitsPoolInitializer : CycleInitializerBase
{
    [Inject] private IUnitFactory _unitFactory;

    protected override void OnInit()
    {
        FWC.GlobalData.UnitsPool = new Pool<UnitBase, UnitID>(CreateUnit);
    }

    private UnitBase CreateUnit(UnitID unitID)
    {
        return _unitFactory.CreateSolid<UnitBase>(unitID);
    }
}