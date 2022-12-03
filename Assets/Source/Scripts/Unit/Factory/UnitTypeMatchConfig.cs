using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "UnitTypeMatchConfig", menuName = "Config/Unit/UnitTypeMatchConfig")]
public class UnitTypeMatchConfig : ScriptableObject, ISingleConfig
{
    [SerializeField] private UnitTypePair[] _unitTypePairs;

    public IReadOnlyDictionary<UnitType, IReadOnlyList<UnitID>> UnitTypePairs { get; private set; }
    public IReadOnlyDictionary<UnitID, UnitType> UnitIDPairs { get; private set; }

    private void OnEnable()
    {
        UnitTypePairs = _unitTypePairs
            ?.ToDictionary(pair => pair.UnitType, pair => (IReadOnlyList<UnitID>)pair.UnitID);

        Dictionary<UnitID, UnitType> ids = new Dictionary<UnitID, UnitType>();

        if (_unitTypePairs != null)
            foreach (UnitTypePair pair in _unitTypePairs)
                foreach (UnitID unitID in pair.UnitID)
                    ids.Add(unitID, pair.UnitType);

        UnitIDPairs = ids;
    }

    public UnitType GetUnitType(UnitID unitID)
    {
        return UnitIDPairs[unitID];
    }


    [Serializable]
    public struct UnitTypePair
    {
        public UnitType UnitType;
        public UnitID[] UnitID;
    }
}