
public class ConstructionBuildingPlayerBlocker : CycleInitializerBase
{
    protected override void OnInit()
    {
        FWC.GlobalData.ConstructionsRepository.OnAdd += OnConstructionAdd;
    }

    private void OnConstructionAdd(ConstructionCellData cellData)
    {
        if (cellData.Construction.ActivityState != ConstructionActivityState.Building_In_Progress)
            return;

        FWC.GlobalData.UserInput.ChangeInputLock(true);
        cellData.Construction.OnActivityStateChange += OnConstructionBuildComplete;
    }

    private void OnConstructionBuildComplete(ConstructionBase construction)
    {
        if (construction.ActivityState is ConstructionActivityState.Building_In_Progress)
            return;

        construction.OnActivityStateChange -= OnConstructionBuildComplete;
        FWC.GlobalData.UserInput.ChangeInputLock(false);
    }
}