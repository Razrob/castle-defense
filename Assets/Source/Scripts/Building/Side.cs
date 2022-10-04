using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public struct Side
{
    [SerializeField] private SideName _sideName;
    [SerializeField] private SideIdentifier _sideIdentifier;

    public SideName SideName => _sideName;
    public ISideIdentifier SideIdentifier => _sideIdentifier;

    private static readonly IReadOnlyList<SideName> _rotateXSides = 
        new SideName[] { SideName.Forward, SideName.Top, SideName.Back, SideName.Bottom };

    private static readonly IReadOnlyList<SideName> _rotateYSides =
        new SideName[] { SideName.Forward, SideName.Left, SideName.Back, SideName.Right };

    private static readonly IReadOnlyList<SideName> _rotateZSides =
        new SideName[] { SideName.Right, SideName.Bottom, SideName.Left, SideName.Top };
     
    public Side(SideName sideName, ISideIdentifier sideIdentifier)
    {
        _sideName = sideName;
        _sideIdentifier = (SideIdentifier)sideIdentifier;
    }

    public static SideName RotateSideName(SideName sideName, Vector3Int euler)
    {
        euler = euler.Round(90);

        int rotatesXCount = euler.x / 90;
        int rotatesYCount = euler.y / 90;
        int rotatesZCount = euler.z / 90;

        TryRotate(_rotateXSides, rotatesXCount);
        TryRotate(_rotateYSides, rotatesYCount);
        TryRotate(_rotateZSides, rotatesZCount);

        void TryRotate(IReadOnlyList<SideName> sideNames, int rotatesCount)
        {
            if (sideNames.Contains(sideName))
            {
                int index = sideNames.IndexOf(side => side == sideName);
                sideName = sideNames[(index + rotatesCount) % sideNames.Count];
            }
        }

        return sideName;
    }
}