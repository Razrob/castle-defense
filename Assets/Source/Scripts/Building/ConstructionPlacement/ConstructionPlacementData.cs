using System;

public class ConstructionPlacementData
{
    public PlacementMode PlacementMode { get; private set; }
    public ConstructionPlacementInfo CurrentPlacementInfo { get; private set; }

    public event Action OnPlacementModeChange;

    public void ChangePlacementMode(PlacementMode placementMode, ConstructionConfiguration<IConstruction>? configuration)
    {
        if (PlacementMode == placementMode 
            && (CurrentPlacementInfo != null == configuration.HasValue))
            return;

        PlacementMode = placementMode;
        CurrentPlacementInfo = configuration.HasValue ? new ConstructionPlacementInfo(configuration.Value) : null;

        OnPlacementModeChange?.Invoke();
    }
}
