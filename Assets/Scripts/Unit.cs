using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public string unitName;
    public int unitLevel;

    public float damage;

    public float maxHP;
    public float currentHP;

    public bool TakeDamage(float damageTaken)
    {
        currentHP -= damageTaken;

        if (currentHP <= 0)
            return true;
        else
            return false;
    }
}
