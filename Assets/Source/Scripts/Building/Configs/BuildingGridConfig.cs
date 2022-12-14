using UnityEngine;

[CreateAssetMenu(fileName = "BuildingGridConfig", menuName = "Config/BuildingGridConfig")]
public class BuildingGridConfig : ScriptableObject, ISingleConfig
{
    [SerializeField] private int _gridTileSize;
    [SerializeField] private Area2Int _gridArea;
    
    public int GridTileSize => _gridTileSize;
    public Area2Int GridArea => _gridArea;
}
