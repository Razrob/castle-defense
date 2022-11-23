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
            ChangePlacementMode(PlacementMode.Proccess, _constructionPlacementScreen.SelectedConstructionID);

        _constructionPlacementScreen.OnActiveChange += value =>
        {
            if (!value)
                ChangePlacementMode(PlacementMode.Empty, null);
        };

        _constructionPlacementScreen.OnApply += ApplyPlacement;
    }

    protected override void OnUpdate()
    {
        if (FWC.GlobalData.ConstructionPlacementData.PlacementMode != PlacementMode.Proccess)
            return;

        Vector3 potentialPosition = FWC.GlobalData.PlayerData.Player.transform.position
            + FWC.GlobalData.PlayerData.Player.transform.rotation * _potentialPositionLocalOffcet;

        potentialPosition = FWC.GlobalData.ConstructionsRepository.RoundPositionToGrid(potentialPosition);
        FWC.GlobalData.ConstructionPlacementData.CurrentPlacementInfo.SetPotentialPlacementPosition(potentialPosition);

        FWC.GlobalData.ConstructionPlacementData.CurrentPlacementInfo.Preview.transform.position = potentialPosition;
    }

    private void ChangePlacementMode(PlacementMode placementMode, ConstructionID? constructionID)
    {
        ConstructionConfiguration<IConstruction>? configuration = null;

        if (placementMode is PlacementMode.Proccess)
            configuration = _constructionFactory.GetConfiguration(constructionID.Value);

        FWC.GlobalData.ConstructionPlacementData.ChangePlacementMode(placementMode, configuration);
    }

    private void ApplyPlacement()
    {
        Destroy(FWC.GlobalData.ConstructionPlacementData.CurrentPlacementInfo.Preview.gameObject);

        ConstructionBase construction = _constructionFactory
            .CreateSolid<ConstructionBase>(FWC.GlobalData.ConstructionPlacementData.CurrentPlacementInfo.Configuration.ConstructionID);

        FWC.GlobalData.ConstructionsRepository
            .AddConstruction(FWC.GlobalData.ConstructionPlacementData.CurrentPlacementInfo.PotentialPlacementPosition.ToInt(), construction);

        ChangePlacementMode(PlacementMode.Empty, null);
    }
}
