using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "ProjectileConfig", menuName = "Config/ProjectileConfig")]
public class ProjectileConfig : ScriptableObject, ISingleConfig
{
    [SerializeField] private ProjectileConfiguration[] _projectiles;

    private Dictionary<ProjectileShape, ProjectileConfiguration> _projectilesDictionary;

    private void OnEnable()
    {
        _projectilesDictionary = _projectiles?.ToDictionary(data => data.Prefab.Identifier, data => data);
    }

    public Projectile Create(ProjectileShape projectileShape)
    {
        if (!_projectilesDictionary.ContainsKey(projectileShape))
            throw new InvalidOperationException();

        Projectile projectile = Instantiate(_projectilesDictionary[projectileShape].Prefab);
        return projectile;
    }


    [Serializable]
    private struct ProjectileConfiguration
    {
        public Projectile Prefab;
    }
}
