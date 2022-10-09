using UnityEngine;

[CreateAssetMenu(fileName = "EntityStateConfig", menuName = "Config/EntityStateConfig")]
public class EntityStateConfig : ScriptableObject
{
    public enum EntityState {Move,Build,Atack};
    public EntityState[] States;
}

