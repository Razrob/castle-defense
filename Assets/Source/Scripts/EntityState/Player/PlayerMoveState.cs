using UnityEngine;
using System;

public class PlayerMoveState : EntityStateBase
{
    private EntityStateID _entityStateID = EntityStateID.Move;
    public override EntityStateID EntityStateID => _entityStateID;

    private Animator _animator;
    private Player _player;

    public override void OnStateEnter()
    {
        _player = FWC.GlobalData.PlayerData.Player;
        _animator = _player.Animator;
        _animator.Play("Movement");
    }

    public override void OnUpdate()
    {
        _animator.SetFloat("Speed", FWC.GlobalData.PlayerData.Player.CharacterController.velocity.magnitude);
    }
    public override void OnStateExit()
    {
       
    }
}
