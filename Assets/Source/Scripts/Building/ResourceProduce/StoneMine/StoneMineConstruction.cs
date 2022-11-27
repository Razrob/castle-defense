using System;
using System.Collections.Generic;
using UnityEngine;

public class StoneMineConstruction : ResourceProduceConstructionBase
{
    [SerializeField] private StoneMineConstructionSkin _skin;

    private ResourceProduceCore _stoneProduceCore;
    private ResourceProduceConstructionConfig _produceConstructionConfig;

    public override ResourceProduceCoreBase ResourceProduceCoreBase => _stoneProduceCore;
    public override ConstructionID ConstructionID => ConstructionID.Stone_Mine;
    public override ConstructionSkinBase ConstructionSkinBase => _skin;

    [ExecuteHierarchyMethod(HierarchyMethodType.On_Awake)]
    private void OnAwake()
    {
        _produceConstructionConfig = ConfigsRepository.FindConfig<ResourceProduceConstructionConfig>();
        _stoneProduceCore = new ResourceProduceCore(_produceConstructionConfig.GetProcessInfo(ConstructionID, ConstructionLevel));

        SetProduceState(ResourceProduceState.Proccessing);
    }

    [ExecuteHierarchyMethod(HierarchyMethodType.On_Update)]
    private void OnUpdate()
    {
        if (ResourceProduceState != ResourceProduceState.Proccessing)
            return;

        _stoneProduceCore.Tick(Time.deltaTime);
        RefreshVisual();
    }

    private void RefreshVisual()
    {
        _skin.SetVisualRepresentationScale(_stoneProduceCore.ProducedResource.CurrentValue / _stoneProduceCore.ProducedResource.Capacity);
    }
}
