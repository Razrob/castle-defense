using System;

[Serializable]
public class GlobalData
{
    public UserInput UserInput;
    public PlayerData PlayerData;
    public ConstructionsRepository ConstructionsRepository;
    public UnitRepository UnitRepository;
    public ResourceRepository ResourceRepository;
    public CameraPresetsChangeData CameraPresetsChangeData;
    public ConstructionPlacementData ConstructionPlacementData;
    public SpecificFormConstructionTransformer FormConstructionTransformer;

    public Pool<Projectile, ProjectileShape> ProjectilesPool;

    public GlobalData()
    {
        UserInput = new UserInput();
        PlayerData = new PlayerData();
        ConstructionsRepository = new ConstructionsRepository();
        UnitRepository = new UnitRepository();
        CameraPresetsChangeData = new CameraPresetsChangeData();
        ConstructionPlacementData = new ConstructionPlacementData();
        FormConstructionTransformer = new SpecificFormConstructionTransformer(ConstructionsRepository);
    }
}