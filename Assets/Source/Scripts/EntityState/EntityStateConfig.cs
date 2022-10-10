using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EntityStateConfig", menuName = "Config/EntityStateConfig")]
public class EntityStateConfig : ScriptableObject
{
    [SerializeField] private EntityStateID[] _states;

    public IReadOnlyList<EntityStateID> States => _states;
}


public enum EntityStateID
{
    Move,
    Build,
    Atack
};
