using Zenject;

public class ProjectilePoolInitializer : CycleInitializerBase
{
    [Inject] private readonly ProjectileConfig _projectileConfig;

    protected override void OnInit()
    {
        FWC.GlobalData.ProjectilesPool = new Pool<Projectile, ProjectileShape>(Create);
    }

    private Projectile Create(ProjectileShape projectileShape)
    {
        return _projectileConfig.Create(projectileShape);
    }
}