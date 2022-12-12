using UnityEngine;
using Random = UnityEngine.Random;

public class Test : IDamageApplicator
{
    public float Damage => 10f;
    public Vector3 DamageDirection => Vector3.zero;
}

public class MortarAttackConstruction : AttackConstruction
{
    [SerializeField] private MortarConstructionSkin _skin;
    [SerializeField] private DefaultHealthBar _healthBar;

    private const float ANGLE = 55f;
    private const float SHOOT_DELAY = 2f;
    private const float SHOOT_DAMAGE = 60f;
    private float _lastShootTimer;

    protected override HealthBarBase _healthBarBase => _healthBar;

    public override ConstructionSkinBase ConstructionSkinBase => _skin;
    public override ConstructionID ConstructionID => ConstructionID.Mortar;

    [ExecuteHierarchyMethod(HierarchyMethodType.On_Update)]
    private void OnUpdate()
    {
        if (Input.GetKeyUp(KeyCode.W))
            TakeDamage(new Test());

        _lastShootTimer += Time.deltaTime;

        if (NearestUnit is null)
            return;

        const float horizontalSpeed = 9f;

        float distance = Vector3.Distance(NearestUnit.transform.position, transform.position);
        float realDuration = distance / horizontalSpeed;

        Vector3 unitHorizontalVelocity = NearestUnit.Velocity.XZ();
        Vector3 targetPosition = NearestUnit.transform.position + unitHorizontalVelocity * realDuration
            + Random.insideUnitSphere * 0.0f;

        Vector3 toUnitDirection = (targetPosition - transform.position.XZ()).normalized;
        Quaternion targetRotation = Quaternion.identity;

        if (toUnitDirection != Vector3.zero)
        targetRotation =
            Quaternion.LookRotation(toUnitDirection, Vector3.up) * Quaternion.Euler(Vector3.right * -ANGLE);

        _skin.MortarFoundation.rotation = 
            Quaternion.Slerp(_skin.MortarFoundation.rotation, Quaternion.LookRotation(toUnitDirection), Time.deltaTime * 6f);
        _skin.MortarGun.rotation = Quaternion.Slerp(_skin.MortarGun.rotation, targetRotation, Time.deltaTime * 6f);

        if (_lastShootTimer >= SHOOT_DELAY)
        {
            _lastShootTimer = 0f;
            Shoot(targetRotation * Vector3.forward, Vector3.Distance(transform.position.XZ(), targetPosition.XZ()));
        }
    }

    private void Shoot(Vector3 direction, float distance)
    {
        if (NearestUnit is null)
            return;

        Projectile projectile = PrepareProjectile(ProjectileShape.Mortar_Projectile, SHOOT_DAMAGE, _skin.BulletSpawnPoint.position);

        float startVelocityMagnitude = Mathf.Sqrt(
            (-Physics.gravity.y * distance) 
            / (2 * Mathf.Sin(Mathf.Deg2Rad * ANGLE) * Mathf.Cos(Mathf.Deg2Rad * ANGLE)));

        projectile.Rigidbody.velocity = direction * startVelocityMagnitude;
    }
}
