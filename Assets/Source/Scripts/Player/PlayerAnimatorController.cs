using UnityEngine;

public class PlayerAnimatorController : CycleInitializerBase
{
    private Animator _animator;
    private EntityStatesHandler _entityStatesHandler;
    private EntityStateConfig.EntityState _entityState;

    private Player _player;
    protected override void OnInit()
    {
        _player = FWC.GlobalData.PlayerData.Player;
        _animator = _player.Animator;
        _entityStatesHandler = _player.EntityStatesHandler;
        _entityState = _entityStatesHandler.StartEntityState;
        _entityStatesHandler.StateOn—hanged += UpdateAnimator;
        UpdateAnimator();
    }

    private void Update()
    {
        UpdateAnimatorParametrs();
    }

    private void UpdateAnimator()
    {
        switch (_entityState)
        {
            case EntityStateConfig.EntityState.Move:
                _animator.Play("Movement");
                break;
        }
    }
    private void UpdateAnimatorParametrs()
    {
        switch (_entityState)
        {
            case EntityStateConfig.EntityState.Move:
                _animator.SetFloat("Speed", FWC.GlobalData.UserInput.JoystickOffcet.magnitude);
                break;
        }
    }
}
