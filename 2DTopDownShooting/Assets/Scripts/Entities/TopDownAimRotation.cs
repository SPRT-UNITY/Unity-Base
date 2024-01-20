using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownAimRotation : MonoBehaviour
{
    [SerializeField] private SpriteRenderer armRenderer;
    [SerializeField] private Transform armPivot;
    [SerializeField] private SpriteRenderer characterRenderer;

    private TopDownCharacterController _characterController;


    private void Awake()
    {
        _characterController = GetComponent<TopDownCharacterController>();
    }
    // Start is called before the first frame update
    void Start()
    {
        _characterController.OnLookEvent += OnAim;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnAim(Vector2 rotation) 
    {
        RotateArm(rotation);
    }

    private void RotateArm(Vector2 rotation) 
    {
        //float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

        //armRenderer.flipY = Mathf.Abs(rotZ) > 90f;
        //characterRenderer.flipX = armRenderer.flipY;

        armPivot.right = rotation;
        armRenderer.flipY = Vector2.Dot(Vector2.right, rotation) < 0;
        characterRenderer.flipX = Vector2.Dot(Vector2.right, rotation) < 0;

        //armPivot.rotation = Quaternion.Euler(0, 0, rotZ);
    }
}
