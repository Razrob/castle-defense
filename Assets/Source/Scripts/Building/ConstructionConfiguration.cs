using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public struct ConstructionConfiguration<TConstruction> : ISerializationCallbackReceiver where TConstruction : IConstruction
{
    [SerializeField] private float _buildingDuration;
    [SerializeField] private ConstructionSkinBase[] _skins;
    [SerializeField] private BuildingProcessSkinBase[] _buildingsSkins;

    public TConstruction ConstructionPrefab;
    public ConstructionPreviewBase Preview;
    public Quaternion Rotation;

    public float BuildingDuration => _buildingDuration;
    public ConstructionID ConstructionID => ConstructionPrefab.ConstructionID;
    public IReadOnlyDictionary<ConstructionLevel, ConstructionSkinBase> Skins { get; private set; }
    public IReadOnlyDictionary<ConstructionLevel, BuildingProcessSkinBase> BuildingProcessSkins { get; private set; }

    public ConstructionConfiguration(TConstruction constructionPrefab, ConstructionPreviewBase preview,
        IEnumerable<ConstructionSkinBase> skins, IEnumerable<BuildingProcessSkinBase> buildingProcessSkins, Quaternion rotation, 
        float buildingDuration) : this()
    {
        _skins = skins.ToArray();
        _buildingsSkins = buildingProcessSkins.ToArray();
        _buildingDuration = buildingDuration;

        Preview = preview;
        ConstructionPrefab = constructionPrefab;
        Rotation = rotation;

        InitSkins();
    }

    public void OnAfterDeserialize()
    {
        try
        {
            InitSkins();
        }
        catch { }
    }

    public void OnBeforeSerialize() { }

    private void InitSkins()
    {
        Skins = _skins?.ToDictionary(skin => skin.ConstructionLevel, skin => skin);
        BuildingProcessSkins = _buildingsSkins?.ToDictionary(skin => skin.ConstructionLevel, skin => skin);
    }

    public static bool operator ==(ConstructionConfiguration<TConstruction> a, ConstructionConfiguration<TConstruction> b)
    {
        return 
            (IConstruction)a.ConstructionPrefab == (IConstruction)b.ConstructionPrefab
            && a.Rotation == b.Rotation;
    }

    public static bool operator !=(ConstructionConfiguration<TConstruction> a, ConstructionConfiguration<TConstruction> b)
    {
        return !(a == b);
    }

    public static implicit operator ConstructionConfiguration<IConstruction>(ConstructionConfiguration<TConstruction> configuration)
    {
        return new ConstructionConfiguration<IConstruction>(
            configuration.ConstructionPrefab,
            configuration.Preview,
            configuration.Skins.Values,
            configuration.BuildingProcessSkins.Values,
            configuration.Rotation,
            configuration.BuildingDuration);
    }
}
