using UnityEngine;

public class DefenceWallConstruction : ConstructionBase, ISpecificFormConstruction
{
    [SerializeField] private DefenceWallSkin _defenceWallSkin;
    [SerializeField] private DefaultHealthBar _healthBar;

    protected override HealthBarBase _healthBarBase => _healthBar;

    public SidesPack SidesPack => _defenceWallSkin.SidesPack;
    public override ConstructionSkinBase ConstructionSkinBase => _defenceWallSkin;
    public override ConstructionID ConstructionID => ConstructionID.Defence_Wall;

    [ExecuteHierarchyMethod(HierarchyMethodType.On_Awake)]
    private void OnAwake()
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

        _defenceWallSkin = (DefenceWallSkin)Instantiate(configuration.Skins[ConstructionLevel.Level_1], 
            transform.position, configuration.Rotation, transform);
        _defenceWallSkin.transform.rotation = configuration.Rotation;
    }
}
