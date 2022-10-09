using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkin : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private GameObject _mesh;
    [SerializeField] private GameObject _armature;
    [SerializeField] private GameObject _armors;

    public Animator Animator => _animator;
    public GameObject Mesh => _mesh;
    public GameObject Armature => _armature;
    public GameObject Armors => _armors;
}
