using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ArcherTowerAttackConstruction : AttackConstruction
{
    [SerializeField] private ArcherTowerConstructionSkin _skin;
    [SerializeField] private DefaultHealthBar _healthBar;

    private const float SHOOT_DELAY = 1.2f;
    private const float BULLET_SPEED = 15f;
    private const float SHOOT_DAMAGE = 26f;
    private float _lastShootTimer;

    private const string ATTACK_TYPE = "AttackType";
    private const int ATTACK_LAYER_INDEX = 2;

    private bool _ropeUpdateRequire => _skin.ArcherAnimator.GetInteger(ATTACK_TYPE) != -1 
        && _skin.ArcherAnimator.GetInteger(ATTACK_TYPE) != 2;

    private Projectile _spawnedRow;
    private Sequence _shootSequence;

    protected override HealthBarBase _healthBarBase => _healthBar;
    public override Vector3 OptionalTimerOffcet => Vector3.up * 0.75f;

    public override ConstructionSkinBase ConstructionSkinBase => _skin;
    public override ConstructionID ConstructionID => ConstructionID.Archer_Tower;

    [ExecuteHierarchyMethod(HierarchyMethodType.On_Start)]
    private void OnStart()
    {
        _skin.ArcherAnimator.SetLayerWeight(ATTACK_LAYER_INDEX, 1f);
        _skin.ArcherAnimator.SetInteger(ATTACK_TYPE, 0);

        _spawnedRow = FWC.GlobalData.ProjectilesPool.ExtractElement(ProjectileShape.Row_Projectile);

        OnConstructionDied += c => _shootSequence?.Kill();
    }

    [ExecuteHierarchyMethod(HierarchyMethodType.On_Update)]
    private void OnUpdate()
    {
        UpdateRope();

        _lastShootTimer += Time.deltaTime;

        if (NearestUnit is null)
            return;

        float distance = Vector3.Distance(NearestUnit.transform.position, transform.position);
        float realDuration = distance / BULLET_SPEED;

        Vector3 unitHorizontalVelocity = NearestUnit.Velocity.XZ();
        Vector3 targetPosition = NearestUnit.transform.position.XZ() + unitHorizontalVelocity * realDuration
            + Random.insideUnitSphere * 0.0f;

        Vector3 toUnitDirection = (targetPosition - transform.position.XZ()).normalized;

        _skin.ArcherAnimator.transform.rotation =
            Quaternion.Slerp(_skin.ArcherAnimator.transform.rotation, Quaternion.LookRotation(toUnitDirection), Time.deltaTime * 9f);
        
        if (_lastShootTimer >= SHOOT_DELAY)
        {
            _lastShootTimer = 0f;
            Shoot(toUnitDirection);
        }
    }

    private void UpdateRope()
    {
        if (!_ropeUpdateRequire)
        {
            _skin.Rope.SetPosition(1, 
                _skin.Rope.transform.InverseTransformPoint((_skin.BowRopePointDown.position + _skin.BowRopePointUp.position) / 2f));
        }
        else
        {
            _skin.Rope.SetPosition(1, _skin.Rope.transform.InverseTransformPoint(_skin.ArcherRightHand.position));
        }

        if (_spawnedRow != null)
        {
            Pose pose = CalculateTargetRowPose(_spawnedRow);
            _spawnedRow.SetPosition(pose.position);
            _spawnedRow.SetRotation(pose.rotation);
        }
    }

    private Pose CalculateTargetRowPose(Projectile row)
    {
        Pose pose = new Pose();

        Vector3 rowDirection = _skin.RowSpawnPoint.position - _skin.ArcherRightHand.position;
        pose.rotation = Quaternion.LookRotation(rowDirection);
        pose.position = _skin.ArcherRightHand.position
            + rowDirection * row.Size.z;

        return pose;
    }

    private void Shoot(Vector3 toUnitDirection)
    {
        Projectile projectile = _spawnedRow;
        _spawnedRow = null;

        projectile.Construct(new ProjectileConstructInfo
        {
            CollisionMask = typeof(IDamagable),
            Damage = SHOOT_DAMAGE,
            Filter = component => !component.CastPossible<ConstructionBase>(),
        });

        projectile.OnCollision += p =>
        {
            p.LastCollisionInfo.Value.TryGetComponent<IDamagable>().TakeDamage(p);
        };

        projectile.Rigidbody.velocity = Vector3.zero;
        projectile.Rigidbody.angularVelocity = Vector3.zero;
        projectile.Rigidbody.AddForce(toUnitDirection * BULLET_SPEED, ForceMode.VelocityChange);

        _skin.ArcherAnimator.SetInteger(ATTACK_TYPE, 1);

        Projectile row = null;

        _shootSequence?.Kill();
        _shootSequence = DOTween.Sequence();

        _shootSequence.AppendInterval(0.167f);
        _shootSequence.AppendCallback(() =>
        {
            _skin.ArcherAnimator.SetInteger(ATTACK_TYPE, 2);

            DOTween.Sequence().AppendInterval(0.09f).AppendCallback(() =>
            {
                if (IsDied)
                    return;

                row = FWC.GlobalData.ProjectilesPool.ExtractElement(ProjectileShape.Row_Projectile);
                row.SetPosition(_skin.ArcherRightHand.transform.position);
                row.SetRotation(Quaternion.LookRotation(-_skin.ArcherRightHand.transform.up));
            })
            .Play();
        });

        _shootSequence.AppendInterval(0.370f).OnUpdate(() =>
        {
            if (row is null)
                return;

            Pose pose = CalculateTargetRowPose(row);
            row.SetPosition(_skin.ArcherRightHand.transform.position);
        });

        _shootSequence.AppendCallback(() =>
        {
            _spawnedRow = row;

            _skin.ArcherAnimator.SetInteger(ATTACK_TYPE, 0);
        });
    }
}
