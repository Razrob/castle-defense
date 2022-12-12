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
    public LevelProgressData LevelProgressData;

    public Pool<ConstructionBase, ConstructionID> ConstructionPool;
    public Pool<Projectile, ProjectileShape> ProjectilesPool;
    public Pool<SpriteTimerBase, TimerType> TimersPool;
    public Pool<UnitBase, UnitID> UnitsPool;

    public GlobalData()
    {
        UserInput = new UserInput();
        PlayerData = new PlayerData();
        ConstructionsRepository = new ConstructionsRepository();
        UnitRepository = new UnitRepository();
        CameraPresetsChangeData = new CameraPresetsChangeData();
        ConstructionPlacementData = new ConstructionPlacementData();
        FormConstructionTransformer = new SpecificFormConstructionTransformer(ConstructionsRepository);
        LevelProgressData = new LevelProgressData();
    }
}