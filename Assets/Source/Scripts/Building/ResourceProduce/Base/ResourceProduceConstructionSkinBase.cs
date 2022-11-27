using UnityEngine;

public abstract class ResourceProduceConstructionSkinBase : ConstructionSkinBase 
{
    [SerializeField] private ParticleSystem _collectEffect;

    public ParticleSystem CollectEffect => _collectEffect;
}