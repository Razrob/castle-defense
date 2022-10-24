using UnityEngine;

public class ResourceBase: ResourceStorage
{
    [SerializeField] private ResourceConfig _config;
    public ResourceConfig Config => _config;
    private ResourceID _id;
    private Sprite _icon;

    public ResourceID ID => _id;
    public Sprite Icon => _icon;

    public ResourceBase(float currentValue, float capacity):base(currentValue, capacity) { }
}
