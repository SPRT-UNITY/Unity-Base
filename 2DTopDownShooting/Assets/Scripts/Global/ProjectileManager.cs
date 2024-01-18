using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    [SerializeField] ParticleSystem _particleSystem;

    public static ProjectileManager Instance;

    [SerializeField] private GameObject _gameObject;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShootBullet(Vector2 startPostition, Vector2 direction, RangedAttackData rangedAttackData) 
    {
        GameObject obj = Instantiate(_gameObject);
        obj.transform.position = startPostition;
        
        RangedAttackController attackController = obj.GetComponent<RangedAttackController>();
        attackController.InitializeAttack(direction, rangedAttackData, this);
        
        obj.SetActive(true);
    }
}
