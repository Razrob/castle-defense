using UnityEngine;
using Zenject;

public class DefenceWallTester : CycleInitializerBase
{
    [Inject] private readonly IConstructionFactory _constructionFactory;

    private SpecificFormConstructionTransformer _formConstructionTransformer;

    protected override void OnInit()
    {
        _formConstructionTransformer = new SpecificFormConstructionTransformer(FWC.GlobalData.ConstructionsRepository);
    }

    protected override void OnUpdate()
    {
        return;

        if (Input.GetMouseButtonUp(0))
        {
            RaycastHit[] raycastHits = Physics.RaycastAll(Camera.main.ScreenPointToRay(Input.mousePosition));
            int index = raycastHits.IndexOf(hit => !hit.collider.isTrigger);

            if (index > -1)
            {
                Vector3 position = FWC.GlobalData.ConstructionsRepository.RoundPositionToGrid(raycastHits[index].point);

                if (FWC.GlobalData.ConstructionsRepository.ConstructionExist(position.ToInt()))
                    return;

                DefenceWallConstruction defenceWall = _constructionFactory.CreateSolid<DefenceWallConstruction>(ConstructionID.Defence_Wall);
                defenceWall.transform.position = position + Vector3.down * 1f;

                FWC.GlobalData.ConstructionsRepository.AddConstruction(position.ToInt(), defenceWall);
                _formConstructionTransformer.Refresh();
            }
        }
    }
}
