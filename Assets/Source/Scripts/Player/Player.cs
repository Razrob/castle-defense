using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private EntityStateMachine _stateMachine;  
    [SerializeField] private float _speed;

    private Rigidbody _rigidbody;
    private Transform _transform;
    private CharacterController _characterController;
    private Animator _animator;

    public EntityStateMachine StateMachine => _stateMachine;
    public float Speed => _speed;
    public Rigidbody Rigidbody => _rigidbody;
    public Transform Transform => _transform;
    public CharacterController CharacterController => _characterController;
    public Animator Animator => _animator;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _transform = GetComponent<Transform>();
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponent<PlayerSkin>().Animator;
    }

    private void Start()
    {
        _stateMachine = new EntityStateMachine(new[] { new PlayerMoveState() }, EntityStateID.Move);
    }
}
