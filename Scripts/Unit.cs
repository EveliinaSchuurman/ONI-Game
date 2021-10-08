using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UnitType
{
    PLAYER, ASSASSIN, DEMON, BOSS
}

public class Unit
{
    public string unitName;
    public int unitLevel;
    public Sprite unitSprite;
    public List<string> unitDialogue = new List<string>();
    public UnitType type;

    public int poisonDuration = 0;
    public float poisonDamage;

    public float maxHP;
    public float currentHP;

    public bool TakeDamage(float damageTaken)
    {
        currentHP -= damageTaken;

        if (currentHP <= 0)
        {
            currentHP = 0;
            return true;
        }
        else
        {
            return false;
        }
    }

    public void Heal(float healAmount)
    {
        currentHP += healAmount;

        if (currentHP > maxHP)
        {
            currentHP = maxHP;
        }

    }
}
