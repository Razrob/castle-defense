using System;
using System.Linq;
using UnityEngine;

public class WarrionAttackUnit : AttackUnit
{
    private ConstructionsRepository _constructionsRepository;
    private AttackUnitsMoveState _moveState;

    protected override void Awake()
    {
        OnInit();
        _constructionsRepository = FWC.GlobalData.ConstructionsRepository;
        _stateMachine = new EntityStateMachine(new EntityStateBase[] { new AttackUnitsMoveState(this), new AttackUnitsAttackState(this) }, EntityStateID.Move);
        _moveState = _stateMachine.GetState<AttackUnitsMoveState>(EntityStateID.Move);
        _updateEvent += OnUpdate;
        _fixedUpdateEvent += OnFixedUpdate;
    }

    private void OnUpdate()
    {

        _stateMachine.OnUpdate();
        ChangeState();
    }

    private void OnFixedUpdate()
    {
        _stateMachine.OnFixedUpdate();
    }

    private void ChangeState()
    {
        if (_moveState.CostructionPathInfo.ConstructionPosition.HasValue)
        {
            Vector3Int targetConstructionPosition = (Vector3Int)_moveState.CostructionPathInfo.ConstructionPosition;
            if (ClosesConstructions.Contains(_constructionsRepository.GetConstruction(targetConstructionPosition)))
            { 
                _stateMachine.SetState(EntityStateID.Attack);
            }
        }
    }

}
