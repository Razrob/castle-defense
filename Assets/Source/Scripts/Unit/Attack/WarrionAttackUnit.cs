using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WarrionAttackUnit : AttackUnit
{
    private ConstructionsRepository _constructionsRepository;
    private AttackUnitsMoveState _moveState;

    private void Awake()
    {
        _constructionsRepository = FWC.GlobalData.ConstructionsRepository;
        _stateMachine = new EntityStateMachine(new EntityStateBase[] { new AttackUnitsMoveState(this), new AttackUnitsAttackState(this) }, EntityStateID.Move);
        _moveState = _stateMachine.GetState<AttackUnitsMoveState>(EntityStateID.Move);
    }

    private void Update()
    {
        _stateMachine.OnUpdate();
        ChangeState();
    }

    private void FixedUpdate()
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
