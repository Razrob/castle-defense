using UnityEngine;
using Zenject;

public class ConstructionBuildingTimerActivator : CycleInitializerBase
{
    [Inject] private IConstructionFactory _constructionFactory;

    protected override void OnInit()
    {
        FWC.GlobalData.ConstructionsRepository.OnAdd += OnConstructionAdd;
    }

    private void OnConstructionAdd(ConstructionCellData cellData)
    {
        if (cellData.Construction.ActivityState != ConstructionActivityState.Building_In_Progress)
            return;

        ConstructionPlacementTimer timer = FWC.GlobalData.TimersPool.ExtractElement(TimerType.Construction_Placement_Timer)
            .Cast<ConstructionPlacementTimer>();

        timer.transform.position = cellData.Construction.transform.position;
        timer.OnComplete += OnComplete;
        timer.StartTimer(_constructionFactory.GetConfiguration(cellData.Construction.ConstructionID).BuildingDuration, 
            cellData.Construction);
    }

    private void OnComplete(SpriteTimerBase timer)
    {
        timer.OnComplete -= OnComplete;
        timer.Cast<ConstructionPlacementTimer>().AttachedConstruction.SetActivityState(ConstructionActivityState.Enabled);
    }
}
