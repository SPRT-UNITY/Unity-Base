using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    [SerializeField] ParticleSystem _impactParticleSystem;

    public static ProjectileManager Instance;

    private ObjectPool _objectPool;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        _objectPool = GetComponent<ObjectPool>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShootBullet(Vector2 startPostition, Vector2 direction, RangedAttackData rangedAttackData) 
    {
        GameObject obj = _objectPool.SpawnFromPool(rangedAttackData.bulletNameTag);
        obj.transform.position = startPostition;
        
        RangedAttackController attackController = obj.GetComponent<RangedAttackController>();
        attackController.InitializeAttack(direction, rangedAttackData, this);
        
        obj.SetActive(true);
    }

    public void CreateImpactParticleAtPosition(Vector3 position, RangedAttackData attackData) 
    {
        _impactParticleSystem.transform.position = position;
        ParticleSystem.EmissionModule em = _impactParticleSystem.emission;
        em.SetBurst(0, new ParticleSystem.Burst(0, Mathf.Ceil(attackData.size * 5)));
        ParticleSystem.MainModule mainModule = _impactParticleSystem.main;
        mainModule.startSpeedMultiplier = attackData.size * 10f;
        _impactParticleSystem.Play();
    }
}
