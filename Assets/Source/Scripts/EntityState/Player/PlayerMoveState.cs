using UnityEngine;
using System;

public class PlayerMoveState : EntityStateBase
{
    private EntityStateID _entityStateID = EntityStateID.Move;
    public override EntityStateID EntityStateID => _entityStateID;

    private Movement _movement;
    private Animator _animator;
    private Player _player;

    public override void OnStateEnter()
    {
        _player = FWC.GlobalData.PlayerData.Player;
        _animator = _player.Animator;
        _movement = new Movement(new CommonStateMovement(), _player.Camera, _player.Speed);
        _animator.Play("Movement");
    }

    public override void OnFixedUpdate() 
    {
        _movement.Move();
    }
 
    public override void OnUpdate()
    {
        _animator.SetFloat("Speed", FWC.GlobalData.PlayerData.Player.CharacterController.velocity.magnitude);
    }
    public override void OnStateExit()
    {
       
    }
}
