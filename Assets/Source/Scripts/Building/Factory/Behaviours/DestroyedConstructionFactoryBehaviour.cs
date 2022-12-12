using Zenject;

public class DestroyedConstructionFactoryBehaviour : ConstructionFactoryBehaviourBase
{
    [Inject] private readonly DestroyedConstructionConfig _config;

    public override ConstructionType ConstructionType => ConstructionType.Destroyed_Construction;

    public override ConstructionConfiguration<IConstruction> GetConfiguration(ConstructionID constructionID)
    {
        ConstructionConfiguration<DestroyedConstructionBase> configuration = _config.GetConfiguration(constructionID);
        ConstructionConfiguration<IConstruction> result = configuration;
        return result;
    }

    public override TConstruction CreateSolid<TConstruction>(ConstructionID constructionID, ConstructionLevel level)
    {
        ConstructionConfiguration<DestroyedConstructionBase> configuration = _config.GetConfiguration(constructionID);

        DestroyedConstructionBase construction = Instantiate(configuration.ConstructionPrefab,
            configuration.ConstructionPrefab.transform.position, configuration.Rotation);

        return construction.Cast<TConstruction>();
    }

    public override TPreview CreatePreview<TPreview>(ConstructionID constructionID)
    {
        throw new System.NotImplementedException();
    }

    public override TSkin CreateSkin<TSkin>(ConstructionID constructionID, ConstructionLevel level)
    {
        throw new System.NotImplementedException();
    }
}
