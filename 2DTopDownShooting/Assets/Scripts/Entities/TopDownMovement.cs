using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownMovement : MonoBehaviour
{
    private TopDownCharacterController _controller;
    private CharacterStatsHandler _stats;

    private Vector2 _moveDirection = Vector2.zero;
    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _controller = GetComponent<TopDownCharacterController>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _stats = GetComponent<CharacterStatsHandler>();
    }

    private void Start()
    {
        _controller.OnMoveEvent += Move;
    }

    private void FixedUpdate()
    {
        ApplyMovement(_moveDirection);
    }

    private void Move(Vector2 direction)
    {
        _moveDirection = direction;
    }

    private void ApplyMovement(Vector2 direction) 
    {
        direction = direction * _stats.currentStats.speed;

        _rigidbody.velocity = direction;
    }
}
