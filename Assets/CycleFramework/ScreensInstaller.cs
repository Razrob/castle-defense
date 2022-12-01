using Zenject;

public class ScreensInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        foreach (UIScreen screen in UIScreenRepository.Screens.Values)
            Container.BindInstance(screen).AsSingle();
    }
}