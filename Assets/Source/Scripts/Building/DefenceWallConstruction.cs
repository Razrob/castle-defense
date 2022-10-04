using UnityEngine;

public class DefenceWallConstruction : ConstructionBase, ISpecificFormConstruction
{
    [SerializeField] private DefenceWallSkin _defenceWallSkin;

    public SidesPack SidesPack => _defenceWallSkin.SidesPack;

    public override ConstructionType ConstructionType => ConstructionType.Defence_Wall;

    protected override void OnAwake()
    {
        _defenceWallSkin = GetComponentInChildren<DefenceWallSkin>(true);
    }

    public ISideIdentifier GetWorldSideIdentifier(SideName worldSideName)
    {
        SideName localSideName = Side.RotateSideName(worldSideName, -_defenceWallSkin.transform.eulerAngles.ToInt());
        return SidesPack.Sides.Find(side => side.SideName == localSideName).SideIdentifier;
    }

    public void SetSides(SidesPack sidesPack)
    {
        if (SidesPack.Compare(sidesPack))
            return;

        Destroy(_defenceWallSkin.gameObject);

        DefenceWallsConfig defenceWallsConfig = ConfigsRepository.FindConfig<DefenceWallsConfig>();
        ConstructionConfiguration<DefenceWallConstruction> configuration;

        if (!defenceWallsConfig.TryFindWall(sidesPack, out configuration))
            configuration = defenceWallsConfig.DefaultWall;

        _defenceWallSkin = (DefenceWallSkin)Instantiate(configuration.SkinPrefab, transform.position, configuration.Rotation, transform);
        _defenceWallSkin.transform.rotation = configuration.Rotation;
    }
}
