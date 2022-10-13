using System;
using UnityEngine;

public class PlayerStateUpdater : CycleInitializerBase
{
    private EntityStateMachine _stateMachine;

    protected override void OnInit()
    {
        _stateMachine = FWC.GlobalData.PlayerData.Player.StateMachine;
    }

    protected override void OnUpdate() 
    {
        _stateMachine.OnUpdate();
    }
}
