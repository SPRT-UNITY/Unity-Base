using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TopDownContactEnemyController : TopDownEnemyController
{
    [SerializeField]
    [Range(0f, 100f)] private float followRange;
    [SerializeField]
    private string targetTag = "Player";
    [SerializeField]
    private SpriteRenderer _spriteRenderer;

    private bool _isCollidingWithTarget = false;

    private HealthSystem _healthSystem;

    [SerializeField]
    private HealthSystem _collidingTargetHealthSystem;
    private TopDownMovement _collidingMovement;

    protected override void Awake()
    {
        base.Awake();
    }
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _healthSystem = GetComponentInChildren<HealthSystem>();
        _healthSystem.OnDamage += OnDamage;
    }


    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        if (_isCollidingWithTarget) 
        {
            ApplyHealthChange();
        }

        Vector2 direction = Vector2.zero;
        if(DistanceToTarget() < followRange) 
        {
            direction = DirectionToTarget();
        }

        CallMoveEvent(direction);
        Rotate(direction);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject receiver = collision.gameObject;

        if (!receiver.CompareTag(targetTag)) 
            return;

        _collidingTargetHealthSystem = receiver.GetComponent<HealthSystem>();
        _collidingMovement = receiver.GetComponent<TopDownMovement>();

        if(_collidingTargetHealthSystem != null) 
        {
            _isCollidingWithTarget = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag(targetTag))
            return;

        _isCollidingWithTarget = false;
    }

    private void ApplyHealthChange() 
    {
        AttackSO attack = Stats.currentStats.attackSO;

        bool hasBeenChanged = _collidingTargetHealthSystem.ChangeHealth(-attack.power);

        if(attack.isOnKnockBack && _collidingMovement != null) 
        {
            _collidingMovement.ApplyKnockback(transform, attack.knockBackPower, attack.knockBackTime);
        }
    }

    private void OnDamage()
    {

    }

    private void Rotate(Vector2 direction) 
    {
        //float rotz = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        //_spriteRenderer.flipX = Mathf.Abs(rotz) > 90;
        _spriteRenderer.flipX = Vector2.Dot(Vector2.right, direction) < 0;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

}
