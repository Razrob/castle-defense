using UnityEngine;

public class GunConstructionSkin : ConstructionSkinBase
{
    [SerializeField] private Transform _gun;
    [SerializeField] private Transform _rotator;
    [SerializeField] private Transform _bulletSpawnPoint;

    public Transform Gun => _gun;
    public Transform Rotator => _rotator;
    public Transform BulletSpawnPoint => _bulletSpawnPoint;
}