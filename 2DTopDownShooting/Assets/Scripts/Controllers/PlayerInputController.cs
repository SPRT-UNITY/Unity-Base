using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : TopDownCharacterController
{
    private Camera _camera;

    protected override void Awake()
    {
        base.Awake();
        _camera = Camera.main;
    }

    public void OnMove(InputValue inputValue)
    {
        // Debug.Log("OnMove" + inputValue.ToString());
        Vector2 moveInput = inputValue.Get<Vector2>().normalized;
        CallMoveEvent(moveInput);
    }
    public void OnLook(InputValue inputValue)
    {
        // Debug.Log("OnLook" + inputValue.ToString());
        Vector2 newAim = inputValue.Get<Vector2>();
        Vector2 worldPos = _camera.ScreenToWorldPoint(newAim);
        newAim = worldPos - ((Vector2)transform.position).normalized;

        if(newAim.magnitude >= .9f) 
        {
            CallLookEvent(newAim);
        }
    }
    public void OnFire(InputValue inputValue)
    {
        IsAttacking = inputValue.isPressed;
    }
}
