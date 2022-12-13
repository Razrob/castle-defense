using Zenject;

public class BuilderHouseConstructionFactoryBehaviour : ConstructionFactoryBehaviourBase
{
    [Inject] private readonly BuilderHouseConstructionConfig _config;

    public override ConstructionType ConstructionType => ConstructionType.Builder_House;

    public override ConstructionConfiguration<IConstruction> GetConfiguration(ConstructionID constructionID)
    {
        ConstructionConfiguration<BuilderHouseConstruction> configuration = _config.Prefab;
        return configuration;
    }

    public override TConstruction CreateSolid<TConstruction>(ConstructionID constructionID, ConstructionLevel level)
    {
        ConstructionConfiguration<BuilderHouseConstruction> configuration = _config.Prefab;

        BuilderHouseConstruction construction = Instantiate(configuration.ConstructionPrefab,
            _config.BuilderHousePosition, configuration.Rotation);

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
