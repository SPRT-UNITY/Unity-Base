using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : TopDownCharacterController
{
    PlayerUICanvas playerUICanvas;
    // Start is called before the first frame update
    public string playerName { get; private set; }

    private void Awake()
    {
        playerUICanvas = GetComponentInChildren<PlayerUICanvas>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMove(InputValue inputValue)
    {
        Vector2 vector2 = inputValue.Get<Vector2>().normalized;
        CharacterMove(vector2);
    }

    public void OnLook(InputValue inputValue) 
    {
        Vector2 aim = inputValue.Get<Vector2>();
        Vector2 aimPos = Camera.main.ScreenToWorldPoint(aim);
        aim = aimPos - (Vector2)transform.position;

        CharacterLook(aim);
    }

    public void OnInteract(InputValue inputValue) 
    {
        
    }

    public void ChangePlayerName(string playerName) 
    {
        this.playerName = playerName;
        playerUICanvas.SetPlayerName();
    }

    public void ChangeCharacter(SpriteRenderer spriteRenderer, Animator animator) 
    {
        GetComponentInChildren<SpriteRenderer>().sprite = spriteRenderer.sprite;
        GetComponentInChildren<Animator>().runtimeAnimatorController = animator.runtimeAnimatorController;
    }
}
