using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelConfig", menuName = "Config/Level/LevelConfig")]
public class LevelConfig : ScriptableObject
{
    [SerializeField] private AttackIteration[] _attackIterations;

    public IReadOnlyList<AttackIteration> AttackIterations => _attackIterations;
}
