using Zenject;

public class TimersPoolInitializer : CycleInitializerBase
{
    [Inject] private TimersConfig _timersConfig;

    protected override void OnInit()
    {
        FWC.GlobalData.TimersPool = new Pool<SpriteTimerBase, TimerType>(Create);
    }

    private SpriteTimerBase Create(TimerType timerType)
    {
        return Instantiate(_timersConfig.GetTimer<SpriteTimerBase>(timerType));
    }
}
