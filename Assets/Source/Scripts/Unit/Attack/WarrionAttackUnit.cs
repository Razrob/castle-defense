using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class WarrionAttackUnit : AttackUnit
{
    private ConstructionsRepository _constructionsRepository;
    private AttackUnitsMoveState _moveState;

    public override UnitID UnitID => UnitID.Warrior;

    [ExecuteHierarchyMethod(HierarchyMethodType.On_Awake)]
    private void OnAwake()
    { 
        _constructionsRepository = FWC.GlobalData.ConstructionsRepository;
        _stateMachine = new EntityStateMachine(new EntityStateBase[] 
        { 
            new AttackUnitsMoveState(this), 
            new AttackUnitsAttackState(this),
            new AttackUnitsDieState(this),
        }, 
        EntityStateID.Move);

        _moveState = _stateMachine.GetState<AttackUnitsMoveState>(EntityStateID.Move);
        _stateMachine.GetState<AttackUnitsAttackStateBase>().OnUnitAttack +=
            () => _moveState.ConstructionPathInfo.Construction?.TakeDamage(new DefaultDamageApplicator(20f));

        OnUnitDied += OnDied;
    }

    [ExecuteHierarchyMethod(HierarchyMethodType.On_Update)]
    private void OnUpdate()
    {
        _stateMachine.OnUpdate();
        ChangeState();
    }

    [ExecuteHierarchyMethod(HierarchyMethodType.On_FixedUpdate)]
    private void OnFixedUpdate()
    {
        _stateMachine.OnFixedUpdate();
    }

    private void OnDied(UnitBase unit, IDamageApplicator damageApplicator)
    {
        _stateMachine.SetState(EntityStateID.Die);
    }

    private void ChangeState()
    {
        if (_moveState.ConstructionPathInfo.ConstructionPosition.HasValue)
        {
            Vector3Int targetConstructionPosition = _moveState.ConstructionPathInfo.ConstructionPosition.Value;

            if (ClosesConstructions.Contains(_constructionsRepository.GetConstruction(targetConstructionPosition)))
            {
                Vector3 constructionSize = _moveState.ConstructionPathInfo.Construction.Bounds.size;
                float angle = Vector3.Angle(_moveState.ConstructionPathInfo.Construction.transform.forward,
                    transform.position - _moveState.ConstructionPathInfo.Construction.transform.position);

                NavMeshAgent.stoppingDistance = 
                    new Vector3(constructionSize.x * Mathf.Cos(Mathf.Deg2Rad * angle), 
                    0f, 
                    constructionSize.z * Mathf.Sin(Mathf.Deg2Rad * angle)).magnitude / 2.4f 
                    + 0.3f;

                _stateMachine.SetState(EntityStateID.Warrior_Attack);
                return;
            }
        }

        _stateMachine.SetState(EntityStateID.Move);
    }
}
