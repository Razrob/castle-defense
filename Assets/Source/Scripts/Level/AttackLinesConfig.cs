using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AttackLinesConfig", menuName = "Config/Level/AttackLinesConfig")]
public class AttackLinesConfig : ScriptableObject, ISingleConfig
{
    [SerializeField] private AttackLineInfo[] _attackLines;

    public IReadOnlyList<AttackLineInfo> AttackLines => _attackLines;
}
