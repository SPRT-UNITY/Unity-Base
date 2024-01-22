using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public class CharacterStatus : MonoBehaviour
{
    public int maxHP;
    public int curHP;
    public float speed;

    public CharacterStatus() 
    {
        maxHP = 100;
        curHP = maxHP;
        speed = 1;
    }

    // CharacterStatus + operator overriding 
    public static CharacterStatus operator +(CharacterStatus s1, CharacterStatus s2) 
    {
        s1.maxHP += s2.maxHP;
        s1.curHP += s2.curHP;
        s1.speed += s2.speed;

        return s1;
    }
}
