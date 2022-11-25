using UnityEngine;

[CreateAssetMenu(fileName = "ResourceConfig", menuName = "Config/ResourceConfig")]

public class ResourceConfig : ScriptableObject
{
    [SerializeField] private ResourceID _id;
    [SerializeField] private Sprite _icon;
    [SerializeField] private float _startValue;
    [SerializeField] private float _capacity;

    public ResourceID ID => _id;
    public Sprite Icon => _icon;
    public float StartValue => _startValue;
    public float Capacity => _capacity;
}