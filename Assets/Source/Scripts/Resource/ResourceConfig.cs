using UnityEngine;

[CreateAssetMenu(fileName = "ResourceConfig", menuName = "Config/ResourceConfig")]

public class ResourceConfig : ScriptableObject
{
    [SerializeField] private ResourceID _id;
    [SerializeField] private Sprite _icon;
    [SerializeField] private float _currentValue;
    [SerializeField] private float _capacity;

    public ResourceID ID => _id;
    public Sprite Icon => _icon;
    public float CurrentValue => _currentValue;
    public float Capacity => _capacity;
}