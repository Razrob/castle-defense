using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct SidesPack
{
    [SerializeField] private Side[] _sides;

    public IReadOnlyList<Side> Sides => _sides;

    public SidesPack(Side[] sides)
    {
        _sides = sides;
    }

    public bool Compare(SidesPack b)
    {
        foreach (Side sideA in Sides)
        {
            if (!b.Sides.Contains(sideB => sideB.SideName == sideA.SideName && sideB.SideIdentifier.Compare(sideA.SideIdentifier)))
                return false;
        }

        return true;
    }

    public SidesPack Rotate(Vector3Int euler)
    {
        Side[] sides = new Side[_sides.Length];

        for (int i = 0; i < sides.Length; i++)
            sides[i] = new Side(Side.RotateSideName(_sides[i].SideName, euler), _sides[i].SideIdentifier);

        return new SidesPack(sides);
    }

    public override string ToString()
    {
        string value = "";

        foreach (Side side in _sides)
            value += $"{side.SideName}: {side.SideIdentifier}\n";

        return value;
    }
}