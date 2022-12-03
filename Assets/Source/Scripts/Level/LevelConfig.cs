using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelConfig", menuName = "Config/Level/LevelConfig")]
public class LevelConfig : ScriptableObject
{
    [SerializeField] private AttackIteration[] _attackIterations;

    public IReadOnlyList<AttackIteration> AttackIterations => _attackIterations;

    public int CommonUnitsCount => _attackIterations.Aggregate(0, (commonCount, iteration) => commonCount + iteration.Parts
        .Aggregate(0, (count, part) => part.Count));
}
