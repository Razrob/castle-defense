using UnityEngine;

public class DefenceWallSkin : ConstructionSkinBase
{
    [SerializeField] private SidesPack _sidesPack;
    public SidesPack SidesPack => _sidesPack;
}