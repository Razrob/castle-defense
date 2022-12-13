using DG.Tweening;
using System;

public class LevelLoser : CycleInitializerBase
{
    protected override void OnInit()
    {
        BuilderHouseConstruction builderHouseConstruction = 
            FWC.GlobalData.ConstructionsRepository.GetConstruction<BuilderHouseConstruction>();

        if (builderHouseConstruction != null)
            OnBuilderHouseCreate(FWC.GlobalData.ConstructionsRepository.Constructions[builderHouseConstruction.transform.position.ToInt()]);

        FWC.GlobalData.ConstructionsRepository.OnAdd += OnBuilderHouseCreate;
    }

    private void OnBuilderHouseCreate(ConstructionCellData data)
    {
        if (data.Construction.ConstructionID != ConstructionID.Builder_House)
            return;

        data.Construction.OnConstructionDied += OnBuilderHouseDied;
    }

    private void OnBuilderHouseDied(ConstructionBase construction)
    {
        construction.OnConstructionDied -= OnBuilderHouseDied;

        DOTween.Sequence().AppendInterval(5f).AppendCallback(() =>
        {
            FWC.GlobalData.LevelProgressData.LoseLevel();
        });
    }
}