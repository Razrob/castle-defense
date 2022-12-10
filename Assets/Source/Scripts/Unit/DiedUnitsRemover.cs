using DG.Tweening;

public class DiedUnitsRemover : CycleInitializerBase
{
    private const float REMOVE_TIMER = 10.0f;

    protected override void OnInit()
    {
        FWC.GlobalData.UnitRepository.OnUnitDied += OnUnitDied;
    }

    private void OnUnitDied(UnitBase unit)
    {
        FWC.GlobalData.UnitRepository.TryGetUnit<UnitBase>(unit.UnitType, u => u == unit, true);
        DOTween.Sequence().AppendInterval(REMOVE_TIMER).AppendCallback(() => RemoveUnit(unit));
    }

    private void RemoveUnit(UnitBase unit)
    {
        unit.SetAlfa(0f, 2f, () => unit.Return());
    }
}