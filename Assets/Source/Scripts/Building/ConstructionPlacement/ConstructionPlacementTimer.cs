
public class ConstructionPlacementTimer : SpriteTimerBase
{
    public ConstructionBase AttachedConstruction { get; private set; }
    public override TimerType TimerType => TimerType.Construction_Placement_Timer;

    public void StartTimer(float duration, ConstructionBase attachedConstruction)
    {
        AttachedConstruction = attachedConstruction;
        StartTimer(duration);
    }

    protected override void OnElementReturnCallback()
    {
        AttachedConstruction = null;
    }
}