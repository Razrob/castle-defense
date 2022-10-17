using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarrionAttackUnit : AttackUnit
{
    private void Awake()
    {
        _stateMachine = new EntityStateMachine(new[] { new WarrionMoveState(this) }, EntityStateID.Move);
    }
}
