using System;
using UnityEngine;

public class ConstructionPlacementData
{
    public PlacementMode PlacementMode { get; private set; }
    public ConstructionPlacementInfo CurrentPlacementInfo { get; private set; }

    public event Action OnPlacementModeChange;

    public void ChangePlacementMode(PlacementMode placementMode, ConstructionConfiguration<IConstruction>? configuration)
    {
        PlacementMode = placementMode;

        if (CurrentPlacementInfo != null)
        {
            GameObject.Destroy(CurrentPlacementInfo.Preview.gameObject);
        }

        CurrentPlacementInfo = configuration.HasValue ? new ConstructionPlacementInfo(configuration.Value) : null;

        OnPlacementModeChange?.Invoke();
    }
}
