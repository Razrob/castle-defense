using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AttackLinesConfig", menuName = "Config/Level/AttackLinesConfig")]
public class AttackLinesConfig : ScriptableObject, ISingleConfig
{
    [SerializeField] private Vector3 _startPosition;
    [SerializeField] private Vector3 _nextPointOffcet;
    [SerializeField] private Vector3 _lineDirection;
    [SerializeField] private int _linesCount;

    public IReadOnlyList<AttackLineInfo> AttackLines { get; private set; }

    private void OnEnable()
    {
        AttackLineInfo[] lines = new AttackLineInfo[_linesCount];

        lines[0] = new AttackLineInfo { StartPosition = _startPosition, Direction = _lineDirection };

        for (int i = 1; i < lines.Length; i++)
            lines[i] = new AttackLineInfo { StartPosition = lines[i - 1].StartPosition + _nextPointOffcet, Direction = _lineDirection };

        AttackLines = lines;
    }
}
