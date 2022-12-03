using Zenject;

public class FactoriesInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        IConstructionFactory constructionFactory = FindObjectOfType<ConstructionFactory>(true);
        Container.BindInstance(constructionFactory).AsSingle();

        IUnitFactory unitFactory = FindObjectOfType<UnitFactory>(true);
        Container.BindInstance(unitFactory).AsSingle();
    }
}