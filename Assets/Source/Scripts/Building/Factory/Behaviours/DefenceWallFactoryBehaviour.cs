using System;
using Zenject;

public class DefenceWallFactoryBehaviour : ConstructionFactoryBehaviourBase
{
    [Inject] private readonly DefenceWallsConfig _defenceWallsConfig;

    public override ConstructionType ConstructionType => ConstructionType.Defence_Wall;

    public override TConstruction Create<TConstruction>(ConstructionID constructionID)
    {
        ConstructionConfiguration<DefenceWallConstruction> configuration = _defenceWallsConfig.DefaultWall;

        DefenceWallConstruction construction = Instantiate(configuration.ConstructionPrefab,
            configuration.ConstructionPrefab.transform.position, configuration.Rotation);

        return construction.Cast<TConstruction>();
    }
}
