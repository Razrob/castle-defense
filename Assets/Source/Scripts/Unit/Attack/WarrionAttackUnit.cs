using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarrionAttackUnit : AttackUnit
{
    private void Awake()
    {
        _stateMachine = new EntityStateMachine(new[] { new AtackUnitsMoveState(this)}, EntityStateID.Move);
    }

    protected override void OnUpdate()
    {
        ChangeState();
    }

    private void ChangeState()
    {
      //  if (CostructionPathInfo.position in ClosesConstructions) 
     //      _stateMachine.SetState(EntityStateID.Atack);
    }

}
