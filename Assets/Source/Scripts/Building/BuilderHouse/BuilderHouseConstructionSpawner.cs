using DG.Tweening;
using UnityEngine;
using Zenject;

public class BuilderHouseConstructionSpawner : CycleInitializerBase
{
    [Inject] private IConstructionFactory _constructionFactory;
    [Inject] private BuilderHouseConstructionConfig _config;
    [Inject] private EaseExtensionsConfig _easeExtensionsConfig;

    protected override void OnInit()
    {
        TrySpawn();
        FWC.GlobalData.LevelProgressData.OnLevelStart += TrySpawn;
    }

    private void TrySpawn()
    {
        BuilderHouseConstruction builderHouse = FWC.GlobalData.ConstructionsRepository.GetConstruction<BuilderHouseConstruction>();

        if (builderHouse != null)
            return;

        builderHouse = _constructionFactory.CreateSolid<BuilderHouseConstruction>(ConstructionID.Builder_House);
        FWC.GlobalData.ConstructionsRepository.AddConstruction(builderHouse.transform.position.ToInt(), builderHouse);

        foreach (Vector3Int position in _config.BusyCells)
            FWC.GlobalData.ConstructionsRepository.BlockCell(position);

        Vector3 scale = builderHouse.transform.localScale;
        builderHouse.transform.localScale = Vector3.zero;
        builderHouse.transform.DOScale(scale, 0.6f)
            .SetEase(_easeExtensionsConfig.EaseExtensions[EaseExtensionType.EaseInCubicOutBack].Curve);
    }
}
