using System;
using Zenject;

public class DefenceWallFactoryBehaviour : ConstructionFactoryBehaviourBase
{
    [Inject] private readonly DefenceWallsConfig _defenceWallsConfig;

    public override ConstructionType ConstructionType => ConstructionType.Defence_Wall;

    public override ConstructionConfiguration<IConstruction> GetConfiguration(ConstructionID constructionID)
    {
        return _defenceWallsConfig.DefaultWall;
    }

    public override TConstruction CreateSolid<TConstruction>(ConstructionID constructionID, ConstructionLevel level)
    {
        ConstructionConfiguration<DefenceWallConstruction> configuration = _defenceWallsConfig.DefaultWall;

        DefenceWallConstruction construction = Instantiate(configuration.ConstructionPrefab,
            configuration.ConstructionPrefab.transform.position, configuration.Rotation);

        return construction.Cast<TConstruction>();
    }

    public override TPreview CreatePreview<TPreview>(ConstructionID constructionID)
    {
        ConstructionConfiguration<DefenceWallConstruction> configuration = _defenceWallsConfig.DefaultWall;

        ConstructionPreviewBase preview = Instantiate(configuration.Preview,
            configuration.Preview.transform.position, configuration.Rotation);

        return preview.Cast<TPreview>();
    }

    public override TSkin CreateSkin<TSkin>(ConstructionID constructionID, ConstructionLevel level)
    {
        throw new System.NotImplementedException();
    }
}
