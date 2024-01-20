using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttackController : MonoBehaviour
{
    [SerializeField] private LayerMask levelCollisionLayer;

    private RangedAttackData _attackData;
    private float _currentDuration;
    private Vector2 _direction;
    private bool _isReady;

    private Rigidbody2D _rigidbody;
    private SpriteRenderer _spriteRenderer;
    private TrailRenderer _trailRenderer;
    private ProjectileManager _projectileManager;

    public bool fxOnDestroy = true;

    private void Awake()
    {
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _trailRenderer = GetComponent<TrailRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!_isReady) return;

        _currentDuration += Time.deltaTime;

        if(_currentDuration > _attackData.duration)
        {
            DestroyProjectile(transform.position, false);
        }

        _rigidbody.velocity = _direction * _attackData.speed;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(levelCollisionLayer.value == (levelCollisionLayer.value | (1 << collider.gameObject.layer))) 
        {
            DestroyProjectile(collider.ClosestPoint(transform.position) - _direction * .2f, fxOnDestroy);
        }
        else if(_attackData.target.value == (_attackData.target.value | (1 << collider.gameObject.layer))) 
        {
            HealthSystem healthSystem = collider.GetComponent<HealthSystem>();
            if(healthSystem != null) 
            {
                healthSystem.ChangeHealth(-_attackData.power);
                if(_attackData.isOnKnockBack) 
                {
                    TopDownMovement movement = collider.GetComponent<TopDownMovement>();
                    if(movement != null) 
                    {
                        movement.ApplyKnockback(transform, _attackData.knockBackPower, _attackData.knockBackTime);
                    }
                }
            }
            DestroyProjectile(collider.ClosestPoint(transform.position), fxOnDestroy);
        }
    }

    public void InitializeAttack(Vector2 direction, RangedAttackData rangedAttackData, ProjectileManager projectileManager) 
    {
        _projectileManager = projectileManager;
        _attackData = rangedAttackData;
        _direction = direction;

        _trailRenderer.Clear();
        _currentDuration = 0;
        _spriteRenderer.color = _attackData.projectileColor;
 
        transform.right = direction;

        _isReady = true;
    }

    private void UpdateProjectileSprite() 
    {
        transform.localScale = Vector3.one * _attackData.size;
    }

    private void DestroyProjectile(Vector3 position, bool createFx)
    {
        if (createFx) 
        {
            _projectileManager.CreateImpactParticleAtPosition(position, _attackData);
        }
        gameObject.SetActive(false);
    }
}
