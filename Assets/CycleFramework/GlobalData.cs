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

    public Pool<Projectile, ProjectileShape> ProjectilesPool;

    public GlobalData()
    {
        UserInput = new UserInput();
        PlayerData = new PlayerData();
        ConstructionsRepository = new ConstructionsRepository();
        UnitRepository = new UnitRepository();
        CameraPresetsChangeData = new CameraPresetsChangeData();
    }
}