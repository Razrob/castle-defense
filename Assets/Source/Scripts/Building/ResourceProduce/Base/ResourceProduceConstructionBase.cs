using System;
using UnityEngine;

public abstract class ResourceProduceConstructionBase : ConstructionBase
{
    [SerializeField] private CollectResourceTriggerBehaviour _collectTriggerBehaviour;
    [SerializeField] private ResourceCollectTimerBase _collectTimer;
    [SerializeField] private ResourceProduceProcessPanelBase _processPanel;

    public abstract ResourceProduceCoreBase ResourceProduceCoreBase { get; }
    public ResourceProduceState ResourceProduceState { get; private set; } = ResourceProduceState.Completed;
    public ResourceID ProduceResoureID => ResourceProduceCoreBase.TargetResourceID;

    public event Action<ResourceProduceConstructionBase> OnProduceStateChange;

    [ExecuteHierarchyMethod(HierarchyMethodType.On_Awake)]
    private void OnAwake()
    {
        _processPanel.SetActive(false);
        _collectTimer.StopTimer();
    }

    [ExecuteHierarchyMethod(HierarchyMethodType.On_Start)]
    private void OnStart()
    {
        ResourceProduceCoreBase.ProducedResource.OnResourceChange += RefreshProducePanel; 
        ResourceProduceCoreBase.ProducedResource.OnCapacityChange += RefreshProducePanel;

        _collectTriggerBehaviour.EnterEvent += triggerable => OnResourceCollectStart(triggerable.Cast<Player>());
        _collectTriggerBehaviour.ExitEvent += triggerable => OnResourceCollectAbort(triggerable.Cast<Player>());

        _processPanel.SetActive(true);
    }

    private void OnResourceCollectStart(Player player) 
    {
        _collectTimer.OnComplete += CollectResource;

        _collectTimer.StartTimer(ConfigsRepository.FindConfig<ResourceProduceConstructionConfig>()
            .GetConstructionInfo(ConstructionID, ConstructionLevel).CollectTime);
    }

    private void OnResourceCollectAbort(Player player) 
    { 
        _collectTimer.OnComplete -= CollectResource;
        _collectTimer.StopTimer();
    }

    private void CollectResource(SpriteTimerBase timer)
    {
        _collectTimer.OnComplete -= CollectResource;
        FWC.GlobalData.ResourceRepository.GetResource(ProduceResoureID).ChangeValue(ResourceProduceCoreBase.ExtractAllProducedResources());

        ConstructionSkinBase.Cast<ResourceProduceConstructionSkinBase>().CollectEffect.Play();
    }

    private void RefreshProducePanel()
    {
        _processPanel
            .SetFill(ResourceProduceCoreBase.ProducedResource.CurrentValueInt, (int)ResourceProduceCoreBase.ProducedResource.Capacity);
    }

    public void SetProduceState(ResourceProduceState state)
    {
        if (ResourceProduceState == state)
            return;

        ResourceProduceState = state;
        OnProduceStateChange?.Invoke(this);
    }
}
