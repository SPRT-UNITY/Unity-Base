using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TopDownCharacterController : MonoBehaviour
{
    public Action<Vector2> MoveEvent;
    public Action<Vector2> LookEvent;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CharacterMove(Vector2 direction) 
    {
        MoveEvent?.Invoke(direction);
    }

    public void CharacterLook(Vector2 direction) 
    {
        LookEvent?.Invoke(direction);
    }
}
