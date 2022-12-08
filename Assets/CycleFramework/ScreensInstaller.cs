using Zenject;

public class ScreensInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        foreach (UIScreen screen in UIScreenRepository.Screens.Values)
            Container.Bind(screen.GetType()).FromInstance(screen);
    }
}