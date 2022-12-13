using System.Linq;
using UnityEngine;
using Zenject;

public class ConstructionDestroyer : CycleInitializerBase
{
    protected override void OnInit()
    {
        foreach (ConstructionCellData data in FWC.GlobalData.ConstructionsRepository.Constructions.Values)
            OnConstructionAdd(data);

        FWC.GlobalData.ConstructionsRepository.OnAdd += OnConstructionAdd;
    }

    private void OnConstructionAdd(ConstructionCellData data)
    {
        data.Construction.OnConstructionDied += OnHealthEnd;
    }

    private void OnHealthEnd(ConstructionBase construction)
    {
        construction.OnConstructionDied += OnHealthEnd;

        DestroyedConstructionBase destroyedConstruction =
            FWC.GlobalData.ConstructionPool.ExtractElement(construction.DestroyCaseConstructionID)
            .Cast<DestroyedConstructionBase>();

        destroyedConstruction.transform.position = construction.transform.position;
        destroyedConstruction.Destroy(new ConstructionDestroyInfo
        {
            DestroyDelay = 0f,
            DestroyDuration = 10f,
            PartsLiquidateInterval = 0.05f,
            PartsVelocity = Vector3.down * 8f,
        });

        FWC.GlobalData.ConstructionsRepository.GetConstruction(construction.transform.position.ToInt(), true);
        Destroy(construction.gameObject);
    }
}
