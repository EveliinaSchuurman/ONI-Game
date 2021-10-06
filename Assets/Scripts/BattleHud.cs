using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleHud : MonoBehaviour
{
    public Text HP;
    public Image HPbar;

    public void SetHud(Unit unit)
    {
        SetHP(unit.currentHP, unit.maxHP);
    }

    public void SetHP(float health, float maxHealth)
    {
        HP.text = health + " / " + maxHealth + " HP";
        HPbar.fillAmount = health / maxHealth;
    }
    
}
