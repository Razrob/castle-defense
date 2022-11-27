using Zenject;

public class ResourceProduceConstructionFactoryBehaviour : ConstructionFactoryBehaviourBase
{
    [Inject] private readonly ResourceProduceConstructionConfig _config;

    public override ConstructionType ConstructionType => ConstructionType.Resource_Produce_Construction;

    public override ConstructionConfiguration<IConstruction> GetConfiguration(ConstructionID constructionID)
    {
        ConstructionConfiguration<ResourceProduceConstructionBase> configuration = _config.GetConfiguration(constructionID);
        ConstructionConfiguration<IConstruction> result = configuration;
        return result;
    }

    public override TConstruction CreateSolid<TConstruction>(ConstructionID constructionID, ConstructionLevel level)
    {
        ConstructionConfiguration<ResourceProduceConstructionBase> configuration = _config.GetConfiguration(constructionID);

        ResourceProduceConstructionBase construction = Instantiate(configuration.ConstructionPrefab,
            configuration.ConstructionPrefab.transform.position, configuration.Rotation);

        return construction.Cast<TConstruction>();
    }

    public override TPreview CreatePreview<TPreview>(ConstructionID constructionID)
    {
        ConstructionConfiguration<ResourceProduceConstructionBase> configuration = _config.GetConfiguration(constructionID);

        ConstructionPreviewBase preview = Instantiate(configuration.Preview,
            configuration.Preview.transform.position, configuration.Rotation);

        return preview.Cast<TPreview>();
    }

    public override TSkin CreateSkin<TSkin>(ConstructionID constructionID, ConstructionLevel level)
    {
        throw new System.NotImplementedException();
    }
}
