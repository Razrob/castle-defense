using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceInitializer : CycleInitializerBase
{
    [SerializeField] private ResourceConfig[] _resourceConfigs;
    private void Awake()
    {   
        ResourceRepository resourceRepository = FWC.GlobalData.ResourceRepository;
        resourceRepository = new ResourceRepository(_resourceConfigs);
        resourceRepository.CreateResource(ResourceID.Gold,0,100);
        resourceRepository.CreateResource(ResourceID.Stone, 0, 100);
    }
}
