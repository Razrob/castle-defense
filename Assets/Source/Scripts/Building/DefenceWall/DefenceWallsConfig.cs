using UnityEngine;

[CreateAssetMenu(fileName = "DefenceWallsConfig", menuName = "Config/DefenceWallsConfig")]
public class DefenceWallsConfig : ScriptableObject, ISingleConfig
{
    [SerializeField] private ConstructionConfiguration<DefenceWallConstruction> _defaultWall;
    [SerializeField] private ConstructionConfiguration<DefenceWallConstruction>[] _defenceWallsPrefabs;

    public ConstructionConfiguration<DefenceWallConstruction> DefaultWall => _defaultWall;

    public bool TryFindWall(SidesPack sidesPack, out ConstructionConfiguration<DefenceWallConstruction> configuration)
    {
        for (int i = 0; i < 4; i++)
        {
            Vector3Int euler = Vector3Int.up * i * 90;

            foreach (ConstructionConfiguration<DefenceWallConstruction> defenceWall in _defenceWallsPrefabs)
            {
                SidesPack rotatedSidesPack = defenceWall.ConstructionPrefab.SidesPack.Rotate(euler);

                if (sidesPack.Compare(rotatedSidesPack))
                {
                    configuration = new ConstructionConfiguration<DefenceWallConstruction>(
                        defenceWall.ConstructionPrefab,
                        defenceWall.SkinPrefab,
                        Quaternion.Euler(-euler));

                    return true;
                }
            }
        }

        configuration = default;
        return false;
    }
}
