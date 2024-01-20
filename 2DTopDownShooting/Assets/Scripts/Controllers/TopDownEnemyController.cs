using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownEnemyController : TopDownCharacterController
{
    GameManager gameManager;
    protected Transform ClosestTarget { get; private set; }

    protected override void Awake()
    {
        base.Awake();
    }


    // Start is called before the first frame update
    protected virtual void Start()
    {
        gameManager = GameManager.Instance;
        ClosestTarget = gameManager.Player;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    protected virtual void FixedUpdate() 
    {
    
    }

    protected float DistanceToTarget() 
    {
        return Vector3.Distance(transform.position, ClosestTarget.position);
    }

    protected Vector2 DirectionToTarget() 
    {
        return (ClosestTarget.position - transform.position).normalized;
    }
}
