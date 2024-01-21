using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterStatsHandler : MonoBehaviour
{
    [SerializeField] private CharacterStats baseStats;
    public CharacterStats currentStats { get; private set; }

    public List<CharacterStats> statsModifiers = new List<CharacterStats>();

    private const float MinAttackDelay = 0.03f;
    private const float MinAttackPower = 0.5f;
    private const float MinAttackSize = 0.4f;
    private const float MinAttackSpeed = .1f;

    private const float MinSpeed = 0.8f;

    private const int MinMaxHealth = 5;

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

    public void AddStatModifier(CharacterStats statModifier)
    {
        statsModifiers.Add(statModifier);
        UpdateCharacterStats();
    }

    public void RemoveStatModifier(CharacterStats statModifier)
    {
        statsModifiers.Remove(statModifier);
        UpdateCharacterStats();
    }

    private void UpdateCharacterStats()
    {
        AttackSO attackSO = null;
        if (baseStats.attackSO != null)
        {
            attackSO = Instantiate(baseStats.attackSO);
        }

        currentStats = new CharacterStats { attackSO = attackSO };
        // TODO
        currentStats.statsChangeType = baseStats.statsChangeType;
        currentStats.maxHealth = baseStats.maxHealth;
        currentStats.speed = baseStats.speed;
        UpdateStats((a, b) => b, baseStats);
        if (currentStats.attackSO != null)
        {
            currentStats.attackSO.target = baseStats.attackSO.target;
        }

        foreach (CharacterStats modifier in statsModifiers.OrderBy(o => o.statsChangeType))
        {
            if (modifier.statsChangeType == StatsChangeType.Override)
            {
                UpdateStats((o, o1) => o1, modifier);
            }
            else if (modifier.statsChangeType == StatsChangeType.Add)
            {
                UpdateStats((o, o1) => o + o1, modifier);
            }
            else if (modifier.statsChangeType == StatsChangeType.Multiple)
            {
                UpdateStats((o, o1) => o * o1, modifier);
            }
        }

        LimitAllStats();
    }

    private void UpdateStats(Func<float, float, float> operation, CharacterStats newModifier) 
    {
        currentStats.maxHealth = (int)operation(currentStats.maxHealth, newModifier.maxHealth);
        currentStats.speed = operation(currentStats.speed, newModifier.speed);

        if (currentStats.attackSO == null || newModifier.attackSO == null)
            return;

        UpdateAttackStats(operation, currentStats.attackSO, newModifier.attackSO);

        if (currentStats.attackSO.GetType() != newModifier.attackSO.GetType())
        {
            return;
        }

        switch (currentStats.attackSO)
        {
            case RangedAttackData _:
                ApplyRangedStats(operation, newModifier);
                break;
        }
    }

    private void UpdateAttackStats(Func<float, float, float> operation, AttackSO currentAttack, AttackSO newAttack)
    {
        if (currentAttack == null || newAttack == null)
        {
            return;
        }

        currentAttack.delay = operation(currentAttack.delay, newAttack.delay);
        currentAttack.power = operation(currentAttack.power, newAttack.power);
        currentAttack.size = operation(currentAttack.size, newAttack.size);
        currentAttack.speed = operation(currentAttack.speed, newAttack.speed);
    }

    private void ApplyRangedStats(Func<float, float, float> operation, CharacterStats newModifier)
    {
        RangedAttackData currentRangedAttacks = (RangedAttackData)currentStats.attackSO;

        if (!(newModifier.attackSO is RangedAttackData))
        {
            return;
        }

        RangedAttackData rangedAttacksModifier = (RangedAttackData)newModifier.attackSO;
        currentRangedAttacks.multipleProjectilesAngle =
            operation(currentRangedAttacks.multipleProjectilesAngle, rangedAttacksModifier.multipleProjectilesAngle);
        currentRangedAttacks.spread = operation(currentRangedAttacks.spread, rangedAttacksModifier.spread);
        currentRangedAttacks.duration = operation(currentRangedAttacks.duration, rangedAttacksModifier.duration);
        currentRangedAttacks.numberOfProjectilesPerShot = Mathf.CeilToInt(operation(currentRangedAttacks.numberOfProjectilesPerShot,
            rangedAttacksModifier.numberOfProjectilesPerShot));
        currentRangedAttacks.projectileColor = UpdateColor(operation, currentRangedAttacks.projectileColor, rangedAttacksModifier.projectileColor);
    }

    private Color UpdateColor(Func<float, float, float> operation, Color currentColor, Color newColor)
    {
        return new Color(
            operation(currentColor.r, newColor.r),
            operation(currentColor.g, newColor.g),
            operation(currentColor.b, newColor.b),
            operation(currentColor.a, newColor.a));
    }

    private void LimitStats(ref float stat, float minVal)
    {
        stat = Mathf.Max(stat, minVal);
    }

    private void LimitAllStats()
    {
        if (currentStats == null || currentStats.attackSO == null)
        {
            return;
        }

        LimitStats(ref currentStats.attackSO.delay, MinAttackDelay);
        LimitStats(ref currentStats.attackSO.power, MinAttackPower);
        LimitStats(ref currentStats.attackSO.size, MinAttackSize);
        LimitStats(ref currentStats.attackSO.speed, MinAttackSpeed);
        LimitStats(ref currentStats.speed, MinSpeed);
        currentStats.maxHealth = Mathf.Max(currentStats.maxHealth, MinMaxHealth);
    }
}
