using System.Collections.Generic;
using UnityEngine;

public class SpecificFormConstructionTransformer
{
    private readonly IConstructionGrid _constructionsGrid;

    public SpecificFormConstructionTransformer(IConstructionGrid constructionsGrid)
    {
        _constructionsGrid = constructionsGrid;
    }

    public void Refresh()
    {
        foreach (Vector3Int position in _constructionsGrid.Positions)
        {
            ISpecificFormConstruction construction = _constructionsGrid.GetConstruction(position) as ISpecificFormConstruction;

            if (construction is null)
                continue;

            Vector3Int forward = position + Vector3Int.forward;
            Vector3Int right = position + Vector3Int.right;
            Vector3Int left = position + Vector3Int.left;
            Vector3Int back = position + Vector3Int.back;

            SideIdentifier GetSideIdentifier(Vector3Int vector) => _constructionsGrid.ConstructionExist<ISpecificFormConstruction>(vector)
                ? new SideIdentifier("OtherWall") : new SideIdentifier("Void");

            Side[] targetSides = new Side[4];
            targetSides[0] = new Side(SideName.Forward, GetSideIdentifier(forward));
            targetSides[1] = new Side(SideName.Right, GetSideIdentifier(right));
            targetSides[2] = new Side(SideName.Left, GetSideIdentifier(left));
            targetSides[3] = new Side(SideName.Back, GetSideIdentifier(back));

            SidesPack sidesPack = new SidesPack(targetSides);
            construction.SetSides(sidesPack);
        }
    }
}
