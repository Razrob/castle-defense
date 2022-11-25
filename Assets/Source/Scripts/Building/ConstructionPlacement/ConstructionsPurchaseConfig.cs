using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "ConstructionsPurchaseConfig", menuName = "Config/ConstructionsPurchaseConfig")]
public class ConstructionsPurchaseConfig : ScriptableObject, ISingleConfig
{
    [SerializeField] private PurchaseInfo<ConstructionID, ResourceSpan>[] _prices;

    public IReadOnlyDictionary<ConstructionID, PurchaseInfo<ConstructionID, ResourceSpan>> Prices { get; private set; }

    private void OnEnable()
    {
        Prices = _prices?.ToDictionary(p => p.Identifier, p => p);
    }
}