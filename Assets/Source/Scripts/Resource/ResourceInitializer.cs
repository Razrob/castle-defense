using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceInitializer : CycleInitializerBase
{
    [SerializeField] private ResourceConfig[] _resourceConfigs;
    private void Awake()
    {   
        FWC.GlobalData.ResourceRepository = new ResourceRepository(_resourceConfigs);
    }
}
