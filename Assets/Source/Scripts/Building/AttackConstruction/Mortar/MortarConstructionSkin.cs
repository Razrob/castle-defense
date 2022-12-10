using UnityEngine;

public class MortarConstructionSkin : ConstructionSkinBase
{
    [SerializeField] private Transform _mortarGun;
    [SerializeField] private Transform _mortarFoundation;
    [SerializeField] private Transform _bulletSpawnPoint;

    public Transform MortarGun => _mortarGun;
    public Transform MortarFoundation => _mortarFoundation;
    public Transform BulletSpawnPoint => _bulletSpawnPoint;
}