using Zenject;

public class ConstructionPoolInitializer : CycleInitializerBase
{
    [Inject] private IConstructionFactory _constructionFactory;

    protected override void OnInit()
    {
        FWC.GlobalData.ConstructionPool = new Pool<ConstructionBase, ConstructionID>(Create);
    }

    private ConstructionBase Create(ConstructionID constructionID)
    {
        return _constructionFactory.CreateSolid<ConstructionBase>(constructionID);
    }
}