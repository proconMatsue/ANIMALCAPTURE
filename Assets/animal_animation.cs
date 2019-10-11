using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestReceiveAttack : MonoBehaviour
{
    private Animator animator;
    public Animator _animator { get; set; }

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }
}
