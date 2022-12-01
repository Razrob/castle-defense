using Zenject;

public class LevelChanger : CycleInitializerBase
{
    [Inject] private readonly GameLevelsCollectionConfig _levelsCollection;
    [Inject] private readonly LevelChangeScreen _levelChangeScreen;

    protected override void OnInit()
    {

    }
}
