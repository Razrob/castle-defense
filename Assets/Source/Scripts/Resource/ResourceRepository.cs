using System;
using System.Collections.Generic;

public class ResourceRepository
{
    private Dictionary<ResourceID, ResourceConfig> _configs;
    private Dictionary<ResourceID, GameResource> _resources;

    public Dictionary<ResourceID, GameResource> Resources => _resources;
    public Dictionary<ResourceID, ResourceConfig> Configs => _configs;

    public event Action<GameResource> OnResourceAdd;

    public ResourceRepository() { }

    public ResourceRepository(ResourceConfig[] resourceConfigs)
    {
        _resources = new Dictionary<ResourceID, GameResource>(resourceConfigs.Length);
        _configs = new Dictionary<ResourceID, ResourceConfig>(resourceConfigs.Length);

        foreach (ResourceConfig config in resourceConfigs)
        {
            _configs.Add(config.ID, config);
            CreateResource(config.ID, config.StartValue, config.Capacity);
        }
    }

    private void CreateResource(ResourceID id, float currentValue, float capacity)
    {
        if (!_resources.ContainsKey(id))
        {
            GameResource resource = new GameResource(_configs[id], currentValue, capacity);
            _resources.Add(id, resource);
            OnResourceAdd?.Invoke(resource);
        }
        else
        {
            throw new Exception("Such a resource already exists");
        }
    }

    public GameResource GetResource(ResourceID resourceType)
    {
        if (_resources.TryGetValue(resourceType, out GameResource resource))
            return resource;
        else
            throw new Exception("No resource");
    }

    public bool SubstructAvailable(ResourceSpan resourceSpan)
    {
        foreach (ResourceSpan.Pair pair in resourceSpan.Pairs.Values)
        {
            if (!_resources.ContainsKey(pair.ResourceID) && pair.IntValue > 0)
                return false;

            if (_resources[pair.ResourceID].CurrentValueInt < pair.IntValue)
                return false;
        }

        return true;
    }

    public bool Substruct(ResourceSpan resourceSpan)
    {
        if (!SubstructAvailable(resourceSpan))
            return false;

        foreach (ResourceSpan.Pair pair in resourceSpan.Pairs.Values)
        {
            if (pair.IntValue < 1)
                continue;

            _resources[pair.ResourceID].ChangeValue(-pair.IntValue);
        }

        return true;
    }
}
