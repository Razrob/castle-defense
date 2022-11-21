public class SpecificFormConstructionRefresher : CycleInitializerBase
{
    protected override void OnInit()
    {
        FWC.GlobalData.ConstructionsRepository.OnRepositoryChange += OnRefresh;
    }

    private void OnRefresh()
    {
        FWC.GlobalData.FormConstructionTransformer.Refresh();
    }
}