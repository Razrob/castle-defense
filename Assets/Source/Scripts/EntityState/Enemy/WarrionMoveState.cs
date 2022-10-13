using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarrionMoveState : EntityStateBase
{
    private EntityStateID _entityStateID = EntityStateID.Move;
    public override EntityStateID EntityStateID => _entityStateID;

    private Animator _animator;
    private Player _warrion;

    public override void OnStateEnter()
    {
    //    _player = FWC.GlobalData.PlayerData.Player;
    //   _animator = _player.Animator;
     //   _animator.Play("Movement");
    }

    public override void OnUpdate()
    {
        _animator.SetFloat("Speed", FWC.GlobalData.UserInput.JoystickOffcet.magnitude);
    }
    public override void OnStateExit()
    {

    }
}
