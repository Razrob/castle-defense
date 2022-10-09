using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : CycleInitializerBase
{
    [SerializeField] private float _speed;
    [SerializeField] private Camera _camera;

    private Movement _movement;

    protected override void OnInit() 
    {
        FWC.GlobalData.PlayerData.Player = FindObjectOfType<Player>();
    }

    private void Awake()
    {
        _movement = new Movement(new CommonStateMovement(),_camera,_speed);
    }

    private void Update()
    {
        _movement.Move();
    }
}
