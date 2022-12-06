using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldMineConstruction : ResourceProduceConstructionBase
{
    [SerializeField] private GoldMineConstructionSkin _skin;
    [SerializeField] private DefaultHealthBar _healthBar;

    private ResourceProduceCore _goldProduceCore;
    private ResourceProduceConstructionConfig _produceConstructionConfig;

    protected override HealthBarBase _healthBarBase => _healthBar;

    public override ResourceProduceCoreBase ResourceProduceCoreBase => _goldProduceCore;
    public override ConstructionID ConstructionID => ConstructionID.Gold_Mine;
    public override ConstructionSkinBase ConstructionSkinBase => _skin;

    [ExecuteHierarchyMethod(HierarchyMethodType.On_Awake)]
    private void OnAwake()
    {
        _produceConstructionConfig = ConfigsRepository.FindConfig<ResourceProduceConstructionConfig>();
        _goldProduceCore = new ResourceProduceCore(_produceConstructionConfig.GetProcessInfo(ConstructionID, ConstructionLevel));

        SetProduceState(ResourceProduceState.Proccessing);
    }

    [ExecuteHierarchyMethod(HierarchyMethodType.On_Update)]
    private void OnUpdate()
    {
        if (ResourceProduceState != ResourceProduceState.Proccessing)
            return;

        _goldProduceCore.Tick(Time.deltaTime);
        RefreshVisual();
    }

    private void RefreshVisual()
    {
        _skin.SetVisualRepresentationScale(_goldProduceCore.ProducedResource.CurrentValue / _goldProduceCore.ProducedResource.Capacity);
    }
}
