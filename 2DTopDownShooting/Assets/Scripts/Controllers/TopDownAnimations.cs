using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownAnimations : MonoBehaviour
{
    protected Animator animator;
    protected TopDownCharacterController characterController;
    protected virtual void Awake() 
    {
        animator = GetComponentInChildren<Animator>();
        characterController = GetComponent<TopDownCharacterController>();
    }

}
