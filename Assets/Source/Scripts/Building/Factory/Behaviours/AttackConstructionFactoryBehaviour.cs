using Zenject;

public class AttackConstructionFactoryBehaviour : ConstructionFactoryBehaviourBase
{
    [Inject] private readonly AttackConstructionConfig _attackConstructionConfig;

    public override ConstructionType ConstructionType => ConstructionType.Attack_Construction;

    public override ConstructionConfiguration<IConstruction> GetConfiguration(ConstructionID constructionID)
    {
        ConstructionConfiguration<AttackConstruction> configuration = _attackConstructionConfig.GetConfiguration(constructionID);
        ConstructionConfiguration<IConstruction> result = configuration;
        return result;
    }

    public override TConstruction CreateSolid<TConstruction>(ConstructionID constructionID, ConstructionLevel level)
    {
        ConstructionConfiguration<AttackConstruction> configuration = _attackConstructionConfig.GetConfiguration(constructionID);

        AttackConstruction construction = Instantiate(configuration.ConstructionPrefab,
            configuration.ConstructionPrefab.transform.position, configuration.Rotation);

        return construction.Cast<TConstruction>();
    }

    public override TPreview CreatePreview<TPreview>(ConstructionID constructionID)
    {
        ConstructionConfiguration<AttackConstruction> configuration = _attackConstructionConfig.GetConfiguration(constructionID);

        ConstructionPreviewBase preview = Instantiate(configuration.Preview,
            configuration.Preview.transform.position, configuration.Rotation);

        return preview.Cast<TPreview>();
    }

    public override TSkin CreateSkin<TSkin>(ConstructionID constructionID, ConstructionLevel level)
    {
        throw new System.NotImplementedException();
    }
}
