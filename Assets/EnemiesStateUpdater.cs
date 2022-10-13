using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesStateUpdater : CycleInitializerBase
{
    private EntityStateMachine _stateMachine;
    private UnitRepository _unitRepository;

    protected override void OnInit()
    {
        _stateMachine = FWC.GlobalData.PlayerData.Player.StateMachine;
      //  _unitRepository.
    }

    protected override void OnUpdate()
    {
        _stateMachine.OnUpdate();
    }
}
