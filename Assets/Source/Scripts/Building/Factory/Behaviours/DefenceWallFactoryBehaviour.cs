using System;

public class DefenceWallFactoryBehaviour : ConstructionFactoryBehaviourBase
{
    private DefenceWallsConfig _defenceWallsConfig;

    public override ConstructionType ConstructionType => ConstructionType.Defence_Wall;

    protected override void OnInit()
    {
        _defenceWallsConfig = ConfigsRepository.FindConfig<DefenceWallsConfig>() ?? 
            throw new NullReferenceException($"{typeof(DefenceWallsConfig).Name} not found");
    }

    public override TConstruction Create<TConstruction>()
    {
        ConstructionConfiguration<DefenceWallConstruction> configuration = _defenceWallsConfig.DefaultWall;

        DefenceWallConstruction construction = Instantiate(configuration.ConstructionPrefab,
            configuration.ConstructionPrefab.transform.position, configuration.Rotation);

        return construction.Cast<TConstruction>();
    }
}
