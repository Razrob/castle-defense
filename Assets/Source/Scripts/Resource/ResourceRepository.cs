using System;
using System.Collections.Generic;

public class ResourceRepository
{
    private Dictionary<ResourceID, ResourceBase> _resources;
    public Dictionary<ResourceID, ResourceBase> Resources => _resources;

    public event Action<ResourceBase> OnResourceAdd;

    public ResourceRepository()
    {
        _resources = new Dictionary<ResourceID, ResourceBase>(3);
    }

    public void AddResource(ResourceBase resource)
    {
        if (!_resources.ContainsKey(resource.ID))
        {
            OnResourceAdd?.Invoke(resource);
            _resources.Add(resource.ID, new ResourceBase(resource.Config.CurrentValue, resource.Config.Capacity));
        }

        throw new Exception("Such a resource already exists");
    }

    public ResourceBase GetResource(ResourceID resourceType)
    {
        if (_resources.TryGetValue(resourceType, out ResourceBase resource))
            return resource;
        else
            throw new Exception("No resource");
    }
}
