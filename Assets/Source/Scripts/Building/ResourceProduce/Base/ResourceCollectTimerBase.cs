
public abstract class ResourceCollectTimerBase : SpriteTimerBase 
{
    protected override bool _isPoolable => false;

    public new void StartTimer(float duration)
    {
        base.StartTimer(duration);
    }
}