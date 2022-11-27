using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "ResourceProduceConstructionConfig", menuName = "Config/ResourceProduceConstructionConfig")]
public class ResourceProduceConstructionConfig : ScriptableObject, ISingleConfig
{
    [SerializeField] private ProduceProcessInfoLevels<ResourceProduceProccessInfo>[] _processInfoLevels;
    [SerializeField] private ConstructionConfiguration<ResourceProduceConstructionBase>[] _produceConstructions;

    private IReadOnlyDictionary<ConstructionID, ProduceProcessInfoLevels<ResourceProduceProccessInfo>> _infoLevels;
    private IReadOnlyDictionary<ConstructionID, ConstructionConfiguration<ResourceProduceConstructionBase>> _constructions;

    private void OnEnable()
    {
        _infoLevels = _processInfoLevels?.ToDictionary(c => c.ConstructionID, c => c);
        _constructions = _produceConstructions?.ToDictionary(c => c.ConstructionID, c => c);
    }

    public ConstructionConfiguration<ResourceProduceConstructionBase> GetConfiguration(ConstructionID constructionID)
    {
        if (!_constructions.ContainsKey(constructionID))
            throw new KeyNotFoundException($"Construction \"{constructionID}\" not found");

        return _constructions[constructionID];
    }

    public ResourceProduceProccessInfo GetProcessInfo(ConstructionID constructionID, ConstructionLevel level)
    {
        if (!_infoLevels.ContainsKey(constructionID))
            throw new KeyNotFoundException($"Produce process info \"{constructionID}\" \"{level}\" not found");

        return _infoLevels[constructionID].Data[(int)level - 1];
    }


    [Serializable]
    public struct ProduceProcessInfoLevels<TProcessInfo> where TProcessInfo : ResourceProduceProccessInfoBase
    {
        public ConstructionID ConstructionID;
        public TProcessInfo[] Data;
    }
}

