
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class TopDownShooting : MonoBehaviour
{
    private ProjectileManager _projectileManager;
    private TopDownCharacterController _controller;

    [SerializeField]
    private Transform projectileSpawnPosition;
    
    private Vector2 _aimDirection = Vector2.right;

    public AudioClip shootingClip;

    private void Awake()
    {
        _controller = GetComponent<TopDownCharacterController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _projectileManager = ProjectileManager.Instance;
        _controller.OnAttackEvent += OnShoot;
        _controller.OnLookEvent += OnAim;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnShoot(AttackSO attackSO) 
    {
        RangedAttackData rangedAttackData = attackSO as RangedAttackData;
        float projectileAngleSpace = rangedAttackData.multipleProjectilesAngle;
        int numberOfProjectilePerShot = rangedAttackData.numberOfProjectilsPerShot;
        float minAngle = -(numberOfProjectilePerShot / 2f) * projectileAngleSpace + 0.5f * rangedAttackData.multipleProjectilesAngle;

        for(int i = 0; i < numberOfProjectilePerShot; i++) 
        {
            float angle = minAngle + projectileAngleSpace * i;
            float randomSpread = UnityEngine.Random.Range(-rangedAttackData.spread, rangedAttackData.spread);
            angle += randomSpread;
            CreateProjectile(rangedAttackData, angle);
        }
    }

    private void CreateProjectile(RangedAttackData rangedAttackData, float angle) 
    {
        _projectileManager.ShootBullet(projectileSpawnPosition.position, RotateVector2(_aimDirection, angle), rangedAttackData);

        if (shootingClip)
            SoundManager.PlayClip(shootingClip);
    }

    private static Vector2 RotateVector2(Vector2 v, float degree) 
    {
        return Quaternion.Euler(0,0,degree) * v;
    }

    private void OnAim(Vector2 direction) 
    {
        _aimDirection = direction;
    }
}
