using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "ProjectileConfig", menuName = "Config/ProjectileConfig")]
public class ProjectileConfig : ScriptableObject, ISingleConfig
{
    [SerializeField] private ProjectileData[] _projectilesData;

    private Dictionary<ProjectileShape, ProjectileData> _projectilesDictionary;

    private void OnEnable()
    {
        _projectilesDictionary = _projectilesData?.ToDictionary(data => data.ProjectileShape, data => data);
    }

    public Projectile Create(ProjectileShape projectileShape)
    {
        if (!_projectilesDictionary.ContainsKey(projectileShape))
            throw new InvalidOperationException();

        Projectile projectile = Instantiate(_projectilesDictionary[projectileShape].Prefab);
        return projectile;
    }


    [Serializable]
    private struct ProjectileData
    {
        public ProjectileShape ProjectileShape;
        public Projectile Prefab;
    }
}
