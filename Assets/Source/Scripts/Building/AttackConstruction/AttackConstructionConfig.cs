using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "AttackConstructionConfig", menuName = "Config/AttackConstructionConfig")]
public class AttackConstructionConfig : ScriptableObject, ISingleConfig
{
    [SerializeField] private ConstructionConfiguration<AttackConstruction>[] _attackConstructions;

    private IReadOnlyDictionary<ConstructionID, ConstructionConfiguration<AttackConstruction>> _constructions;

    private void OnEnable()
    {
        _constructions = _attackConstructions?.ToDictionary(c => c.ConstructionID, c => c);
    }

    public ConstructionConfiguration<AttackConstruction> GetConfiguration(ConstructionID constructionID)
    {
        if (!_constructions.ContainsKey(constructionID))
            throw new KeyNotFoundException($"Construction \"{constructionID}\" not found");

        return _constructions[constructionID];
    }
}