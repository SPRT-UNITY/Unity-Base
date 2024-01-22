using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(TopDownCharacterController))]
[RequireComponent(typeof(CharacterStatus))]
[RequireComponent(typeof(Rigidbody2D))]
public class TopDownCharacterMovement : MonoBehaviour
{
    private TopDownCharacterController _characterController;
    private Rigidbody2D _rigidbody;
    private CharacterStatus _status;

    private Vector2 _moveVector;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _characterController = GetComponent<TopDownCharacterController>();
        _status = GetComponent<CharacterStatus>();

        _rigidbody.gravityScale = 0;
        _rigidbody.freezeRotation = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        _characterController.MoveEvent += Move;
    }


    private void FixedUpdate()
    {
        ApplyMovement(_moveVector);
    }

    private void Move(Vector2 direction)
    {
        _moveVector = direction;
    }

    private void ApplyMovement(Vector2 direction) 
    {
        // direction = direction * _status.currentStats.speed;
        _rigidbody.velocity = direction * _status.speed;
    }
}
