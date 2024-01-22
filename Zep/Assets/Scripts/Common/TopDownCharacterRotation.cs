using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TopDownCharacterController))]
public class TopDownCharacterRotation : MonoBehaviour
{
    private SpriteRenderer characterSprite;
    private TopDownCharacterController characterController;

    private void Awake()
    {
        characterSprite = GetComponentInChildren<SpriteRenderer>();
        characterController = GetComponent<TopDownCharacterController>();
    }
    // Start is called before the first frame update
    void Start()
    {
        characterController.LookEvent += OnAim;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnAim(Vector2 look) 
    {
        characterSprite.flipX = Vector2.Dot(Vector2.right, look) < 0;
    }
}
