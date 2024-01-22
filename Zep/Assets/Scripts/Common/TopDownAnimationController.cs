using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

[RequireComponent(typeof(TopDownCharacterController))]
public class TopDownAnimationController : MonoBehaviour
{
    private static readonly int IsWalking = Animator.StringToHash("IsWalking");

    Animator _animator;
    TopDownCharacterController _controller;

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        _controller = GetComponent<TopDownCharacterController>();
    }


    // Start is called before the first frame update
    void Start()
    {
        _controller.MoveEvent += Move;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Move(Vector2 moveVector) 
    {
        _animator.SetBool(IsWalking, moveVector.magnitude > .5f);
    }
}
