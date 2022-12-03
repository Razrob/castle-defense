public class LevelInfo
{
    public readonly LevelConfig LevelConfig;
    public readonly IterationUnitsProcessor IterationUnitsProcessor;

    public LevelInfo(LevelConfig levelConfig)
    {
        LevelConfig = levelConfig;
        IterationUnitsProcessor = new IterationUnitsProcessor(levelConfig);
    }
}