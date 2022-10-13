using UnityEngine;

public class PlayerAnimatorController : CycleInitializerBase
{
    private Animator _animator;
    private EntityStateMachine _stateMachine;

    private Player _player;
    protected override void OnInit()
    {
        _player = FWC.GlobalData.PlayerData.Player;
        _animator = _player.Animator;
        _stateMachine = _player.StateMachine;
    }
}
