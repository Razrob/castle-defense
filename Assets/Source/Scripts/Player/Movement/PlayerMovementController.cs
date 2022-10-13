using UnityEngine;

public class PlayerMovementController : CycleInitializerBase
{
    [SerializeField] private Camera _camera;

    private Movement _movement;

    protected override void OnInit() 
    {
        _movement = new Movement(new CommonStateMovement(), _camera, FWC.GlobalData.PlayerData.Player.Speed);
    }

    private void Update()
    {
        _movement.Move();
    }
}
