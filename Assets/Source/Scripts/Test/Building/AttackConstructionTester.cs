using UnityEngine;
using Zenject;

public class AttackConstructionTester : CycleInitializerBase
{
    [Inject] private readonly IConstructionFactory _constructionFactory;

    //protected override void OnUpdate()
    //{
    //    return;

    //    if (Input.GetMouseButtonUp(1))
    //    {
    //        RaycastHit[] raycastHits = Physics.RaycastAll(Camera.main.ScreenPointToRay(Input.mousePosition));
    //        int index = raycastHits.IndexOf(hit => !hit.collider.isTrigger);

    //        if (index > -1)
    //        {
    //            Vector3 position = FWC.GlobalData.ConstructionsRepository.RoundPositionToGrid(raycastHits[index].point);

    //            if (FWC.GlobalData.ConstructionsRepository.ConstructionExist(position.ToInt()))
    //                return;

    //            MortarAttackConstruction mortar = _constructionFactory.CreateSolid<MortarAttackConstruction>(ConstructionID.Mortar);
    //            mortar.transform.position = position;

    //            FWC.GlobalData.ConstructionsRepository.AddConstruction(position.ToInt(), mortar);
    //        }
    //    }
    //}
}
