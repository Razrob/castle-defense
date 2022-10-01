using UnityEngine;

public class Movement
{
    private Camera _camera;
    public Camera Camera => _camera;

    private float _speed;
    public float Speed => _speed;

    public IMovementState State { get; set; }

    public Movement(IMovementState movementState, Camera camera, float speed)
    {
        State = movementState;
        _camera = camera;
        _speed = speed;
    }
    public void Move()
    {
        State.Move(this);
    }
}
