using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class MortarAttackConstruction : AttackConstruction
{
    [SerializeField] private MortarConstructionSkin _skin;

    private const float _angle = 55f;
    private const float _shootDelay = 2f;
    private float _lastShootTimer;

    public override ConstructionSkinBase ConstructionSkinBase => _skin;
    public override ConstructionID ConstructionID => ConstructionID.Mortar;

    [ExecuteHierarchyMethod(HierarchyMethodType.On_Update)]
    private void OnUpdate()
    {
        _lastShootTimer += Time.deltaTime;

        if (NearestUnit is null)
            return;

        Vector3 unitDirection = (NearestUnit.transform.position.XZ() - transform.position.XZ()).normalized;
        Quaternion targetRotation = new Quaternion();

        if (unitDirection!= Vector3.zero)
        targetRotation =
            Quaternion.LookRotation((NearestUnit.transform.position.XZ() - transform.position.XZ()).normalized, Vector3.up)
            * Quaternion.Euler(Vector3.right * -_angle);

        _skin.MortarGun.rotation = Quaternion.Slerp(_skin.MortarGun.rotation, targetRotation, Time.deltaTime * 6f);

        if (_lastShootTimer >= _shootDelay)
        {
            _lastShootTimer = 0f;
            Shoot();
        }
    }

    private void Shoot()
    {
        if (NearestUnit is null)
            return;

        Projectile projectile = FWC.GlobalData.ProjectilesPool.ExtractElement(ProjectileShape.Sphere_Shape);

        projectile.Construct(typeof(IDamagable), ProjectileShape.Sphere_Shape, 50f);
        projectile.Rigidbody.position = _skin.BulletSpawnPoint.position;
        projectile.OnCollision += p =>
        {
            p.LastCollisionInfo.Value.TryGetComponent<IDamagable>().TakeDamage(p);
        };

        const float horizontalSpeed = 3f;

        float distance = Vector3.Distance(NearestUnit.transform.position, transform.position);
        float realDuration = distance / horizontalSpeed;

        Vector3 unitHorizontalVelocity = NearestUnit.Velocity.XZ();
        Vector3 targetPosition = NearestUnit.transform.position + unitHorizontalVelocity * realDuration + Vector3.up * 1.5f
            + Random.insideUnitSphere * 0.4f;

        Vector3 direction = (targetPosition.XZ() - transform.position.XZ()).normalized;

        float startVelocityMagnitude = Mathf.Sqrt(
            (-Physics.gravity.y * distance) 
            / (2 * Mathf.Sin(Mathf.Deg2Rad * _angle) * Mathf.Cos(Mathf.Deg2Rad * _angle)));

        projectile.Rigidbody.velocity = Quaternion.LookRotation(direction) * ((Quaternion.Euler(-_angle, 0, 0) * 
            Quaternion.Euler(-(Quaternion.LookRotation(direction)).eulerAngles))
            * direction) * startVelocityMagnitude;
    }
}
