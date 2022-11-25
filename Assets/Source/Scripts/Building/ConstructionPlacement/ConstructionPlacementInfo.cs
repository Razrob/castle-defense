using UnityEngine;

public class ConstructionPlacementInfo
{
    public readonly ConstructionConfiguration<IConstruction> Configuration;
    public readonly ConstructionPreviewBase Preview;
    public Vector3? PotentialPlacementPosition { get; private set; }

    public ConstructionPlacementInfo(ConstructionConfiguration<IConstruction> configuration)
    {
        Configuration = configuration;
        Preview = GameObject.Instantiate(configuration.Preview);
    }

    public void SetPotentialPlacementPosition(Vector3? position)
    {
        PotentialPlacementPosition = position;
    }
}