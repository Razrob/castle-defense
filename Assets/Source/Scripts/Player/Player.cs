using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private Transform _transform;
    private CharacterController _characterController;

    public Rigidbody Rigidbody => _rigidbody;
    public Transform Transform => _transform;
    public CharacterController CharacterController => _characterController;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _transform = GetComponent<Transform>();
        _characterController = GetComponent<CharacterController>();
    }
}
