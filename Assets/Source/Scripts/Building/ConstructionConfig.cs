using UnityEngine;

[CreateAssetMenu(fileName = "ConstructionConfig", menuName = "Config/ConstructionConfig")]
public class ConstructionConfig : ScriptableObject
{
    [SerializeField] private int _gridTileSize;

    public int GridTileSize => _gridTileSize;
}

