using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStatsHandler : MonoBehaviour
{
    [SerializeField] private CharacterStats baseStats;
    public CharacterStats currentStats { get; private set; }

    public List<CharacterStats> statsModifiers = new List<CharacterStats>();

    private void Awake()
    {
        UpdateCharacterStats();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void UpdateCharacterStats()
    {
        AttackSO attackSO = null;

        if(baseStats.attackSO != null) 
        {
            attackSO = Instantiate(baseStats.attackSO);
        }

        currentStats = new CharacterStats() { attackSO = attackSO };

        // юс╫ц
        currentStats.statsChangeType = baseStats.statsChangeType;
        currentStats.maxHealth = baseStats.maxHealth;
        currentStats.speed = baseStats.speed;
    }
}
