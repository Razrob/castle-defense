public class BuilderHouseDestroyedConstruction : DestroyedConstructionBase
{
    public override DestroyedConstructionID DestroyedConstructionID => DestroyedConstructionID.Builder_House;

    public override ConstructionID ConstructionID => ConstructionID.Builder_House_Destroyed_Construction;
    public override ConstructionSkinBase ConstructionSkinBase => default;
    protected override HealthBarBase _healthBarBase => default;
}
