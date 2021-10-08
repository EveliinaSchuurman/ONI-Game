using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SkillTypes
{
    BASIC, POWERCOST, POISON, LIFESTEAL, POISONSWORD, MERCY
}

public class Skill : MonoBehaviour
{
    public string skillName;
    public float damage;
    public float heal;
    public float powerCost;
    public int poisonDamage;
    public int poisonDuration;
    public SkillTypes skillType;

}
