using UnityEngine;

public class GameResourcesDisplayUpdater : CycleInitializerBase
{
    private GameResourcesScreen _gameResourcesScreen;

    protected override void OnInit()
    {
        _gameResourcesScreen = UIScreenRepository.GetScreen<GameResourcesScreen>();
        foreach (GameResource gameResource in FWC.GlobalData.ResourceRepository.Resources.Values)
            OnResourceAdd(gameResource);

        FWC.GlobalData.ResourceRepository.OnResourceAdd += OnResourceAdd;

        UpdateAllResourcesDisplay();
    }

    private void OnResourceAdd(GameResource resource)
    {
        resource.OnResourceChange += () => UpdateResourceDisplay(resource);
    }

    private void UpdateResourceDisplay(GameResource resource)
    {
        _gameResourcesScreen.SetResourceCount(resource.ID, resource.CurrentValueInt);
    }

    private void UpdateAllResourcesDisplay()
    {
        foreach (GameResource gameResource in FWC.GlobalData.ResourceRepository.Resources.Values)
            UpdateResourceDisplay(gameResource);

    }
}