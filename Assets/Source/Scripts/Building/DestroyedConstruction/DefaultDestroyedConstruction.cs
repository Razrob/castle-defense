public class DefaultDestroyedConstruction : DestroyedConstructionBase
{
    public override DestroyedConstructionID DestroyedConstructionID => DestroyedConstructionID.Default;

    public override ConstructionID ConstructionID => ConstructionID.Default_Destroyed_Construction;
    public override ConstructionSkinBase ConstructionSkinBase => default;
    protected override HealthBarBase _healthBarBase => default;
}
