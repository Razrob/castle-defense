using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameLevelsCollectionConfig", menuName = "Config/Level/GameLevelsCollectionConfig")]
public class GameLevelsCollectionConfig : ScriptableObject, ISingleConfig
{
    [SerializeField] private LevelConfig[] _levels;

    public IReadOnlyList<LevelConfig> Levels => _levels;
}