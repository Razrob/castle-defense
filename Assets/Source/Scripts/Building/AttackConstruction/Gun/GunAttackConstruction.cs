using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunAttackConstruction : AttackConstruction
{
    [SerializeField] private GunConstructionSkin _skin;
    [SerializeField] private DefaultHealthBar _healthBar;

    private const float SHOOT_DELAY = 0.5f;
    private const float BULLET_SPEED = 20f;
    private const float SHOOT_DAMAGE = 6f;
    private float _lastShootTimer;

    protected override HealthBarBase _healthBarBase => _healthBar;

    public override ConstructionSkinBase ConstructionSkinBase => _skin;
    public override ConstructionID ConstructionID => ConstructionID.Gun;

    [ExecuteHierarchyMethod(HierarchyMethodType.On_Update)]
    private void OnUpdate()
    {
        _lastShootTimer += Time.deltaTime;

        if (NearestUnit is null)
            return;

        float distance = Vector3.Distance(NearestUnit.transform.position, transform.position);
        float realDuration = distance / BULLET_SPEED;

        Vector3 unitHorizontalVelocity = NearestUnit.Velocity.XZ();
        Vector3 targetPosition = NearestUnit.transform.position.XZ() + unitHorizontalVelocity * realDuration
            + Random.insideUnitSphere * 0.0f;

        Vector3 toUnitDirection = (targetPosition - transform.position.XZ()).normalized;

        _skin.Rotator.rotation = Quaternion.LookRotation(toUnitDirection);

        if (_lastShootTimer >= SHOOT_DELAY)
        {
            _lastShootTimer = 0f;
            Shoot(toUnitDirection);
        }
    }

    private void Shoot(Vector3 toUnitDirection)
    {
        Projectile projectile = PrepareProjectile(ProjectileShape.Gun_Projectile, SHOOT_DAMAGE, _skin.BulletSpawnPoint.position);
        projectile.Rigidbody.velocity = toUnitDirection * BULLET_SPEED;
    }
}
