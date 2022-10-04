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
        if (Input.GetMouseButtonUp(0))
        {
            RaycastHit raycastHit;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out raycastHit))
            {
                Vector3 position = FWC.GlobalData.ConstructionsRepository.RoundPositionToGrid(raycastHit.point);

                if (FWC.GlobalData.ConstructionsRepository.ConstructionExist(position.ToInt()))
                    return;

                DefenceWallConstruction defenceWall = _constructionFactory.Create<DefenceWallConstruction>(ConstructionType.Defence_Wall);
                defenceWall.transform.position = position;

                FWC.GlobalData.ConstructionsRepository.AddConstruction(position.ToInt(), defenceWall);
                _formConstructionTransformer.Refresh();
            }
        }
    }
}
