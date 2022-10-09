using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private EntityStateConfig _entityStateConfig;
    [SerializeField] private float _speed;

    private EntityStatesHandler _entityStatesHandler;

    private Rigidbody _rigidbody;
    private Transform _transform;
    private CharacterController _characterController;
    private Animator _animator;

    public EntityStatesHandler EntityStatesHandler => _entityStatesHandler;
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
        _entityStatesHandler = new EntityStatesHandler(_entityStateConfig);
        _entityStatesHandler.StartEntityState = EntityStateConfig.EntityState.Move;
    }
}
