using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody Rigidbody;
    public Transform Transform;
    public CharacterController CharacterController;

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
        Transform = GetComponent<Transform>();
        CharacterController = GetComponent<CharacterController>();
    }
}
