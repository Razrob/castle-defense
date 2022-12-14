using UnityEngine;
using Zenject;

public class ConstructionPlacer : CycleInitializerBase
{
    [SerializeField] private Vector3 _potentialPositionLocalOffcet;

    [Inject] private readonly IConstructionFactory _constructionFactory;

    private ConstructionPlacementScreen _constructionPlacementScreen;

    protected override void OnInit()
    {
        _constructionPlacementScreen = UIScreenRepository.GetScreen<ConstructionPlacementScreen>();

        _constructionPlacementScreen.OnConstructionSelect += () => 
            ChangePlacementMode(PlacementMode.Proccess, _constructionPlacementScreen.SelectedCell.ConstructionID);

        _constructionPlacementScreen.OnActiveChange += value =>
        {
            if (!value)
                ChangePlacementMode(PlacementMode.Empty, null);
        };

        _constructionPlacementScreen.OnTryApply += TryApplyPlacement;
    }

    protected override void OnUpdate()
    {
        if (FWC.GlobalData.ConstructionPlacementData.PlacementMode != PlacementMode.Proccess)
            return;

        Vector3 potentialPosition = FWC.GlobalData.PlayerData.Player.transform.position
            + FWC.GlobalData.PlayerData.Player.transform.rotation * _potentialPositionLocalOffcet;

        potentialPosition = FWC.GlobalData.ConstructionsRepository.RoundPositionToGrid(potentialPosition).SetY(0f);

        if (!FWC.GlobalData.ConstructionsRepository.CellIsAvailable(potentialPosition.ToInt()))
            return;

        FWC.GlobalData.ConstructionPlacementData.CurrentPlacementInfo.SetPotentialPlacementPosition(potentialPosition);
        FWC.GlobalData.ConstructionPlacementData.CurrentPlacementInfo.Preview.transform.position = potentialPosition;
    }

    private void ChangePlacementMode(PlacementMode placementMode, ConstructionID? constructionID)
    {
        if (placementMode is PlacementMode.Proccess
            && FWC.GlobalData.ConstructionsRepository.AnyConstructionInBuilding())
            return;

        ConstructionConfiguration<IConstruction>? configuration = null;

        if (placementMode is PlacementMode.Proccess)
            configuration = _constructionFactory.GetConfiguration(constructionID.Value);

        FWC.GlobalData.ConstructionPlacementData.ChangePlacementMode(placementMode, configuration);
    }

    private bool TryApplyPlacement()
    {
        if (FWC.GlobalData.ConstructionPlacementData.PlacementMode != PlacementMode.Proccess)
            return false;

        if (!FWC.GlobalData.ConstructionPlacementData.CurrentPlacementInfo.PotentialPlacementPosition.HasValue)
            return false;

        if (!FWC.GlobalData.ResourceRepository.SubstructAvailable(_constructionPlacementScreen.SelectedCell.Price))
            return false;

        FWC.GlobalData.ResourceRepository.Substruct(_constructionPlacementScreen.SelectedCell.Price);

        Destroy(FWC.GlobalData.ConstructionPlacementData.CurrentPlacementInfo.Preview.gameObject);

        ConstructionID constructionID = FWC.GlobalData.ConstructionPlacementData.CurrentPlacementInfo.Configuration.ConstructionID;

        ConstructionBase construction = _constructionFactory
            .CreateSolid<ConstructionBase>(constructionID);

        float buildingDuration = _constructionFactory.GetConfiguration(constructionID).BuildingDuration;

        if (buildingDuration > 0f)
            construction.SetActivityState(ConstructionActivityState.Building_In_Progress);

        FWC.GlobalData.ConstructionsRepository
            .AddConstruction(FWC.GlobalData.ConstructionPlacementData.CurrentPlacementInfo.PotentialPlacementPosition.Value.ToInt(),
            construction);

        ChangePlacementMode(PlacementMode.Empty, null);

        return true;
    }
}
