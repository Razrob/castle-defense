using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "UnitConfig", menuName = "Config/Unit/UnitConfig")]
public class UnitConfig : ScriptableObject, ISingleConfig
{
    [SerializeField] private UnitConfiguration<UnitBase>[] _prefabs;

    private IReadOnlyDictionary<UnitID, UnitConfiguration<UnitBase>> _configurations;

    private void OnEnable()
    {
        _configurations = _prefabs?.ToDictionary(c => c.Unit.UnitID, c => c);
    }

    public UnitConfiguration<UnitBase> GetConfiguration(UnitID unitID)
    {
        if (!_configurations.ContainsKey(unitID))
            return default;

        return _configurations[unitID];
    }
}
