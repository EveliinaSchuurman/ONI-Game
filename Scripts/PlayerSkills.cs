using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSkills
{
    private int skillPoints;
    public Dictionary<int, Skilltype> skillIndex = new Dictionary<int, Skilltype>();

    public enum Skilltype
    {
        none,
        sword,
        swordlv2,
        swordlv3,
        swordaltlv2,
        block,
        blocklv2,
        blocklv3,
        flames,
        flameslv2,
        flameslv3,
        poison,
        poisonlv2
    }

    private List<Skilltype> unlockedSkilltypesList;
    public PlayerSkills()
    {
        unlockedSkilltypesList = new List<Skilltype>();
    }

    public int GetSkillPoints()
    {
        return skillPoints;
    }
    public void SetSkillpoints(int skillpoints)
    {
        skillPoints -= skillpoints;
    }

    public void LoadSkillpoints(int points)
    {
        skillPoints = points;
    }

    private void UnlockSkill(Skilltype skilltype)
    {
        if (!isSkillUnlocked(skilltype) && skilltype != Skilltype.sword && skilltype != Skilltype.block)
        {
            
            unlockedSkilltypesList.Add(skilltype);
        }
        else { unlockedSkilltypesList.Add(skilltype); }
    }

    public bool isSkillUnlocked(Skilltype skilltype)
    {
        return unlockedSkilltypesList.Contains(skilltype);
    }

    public bool CanUnlock(Skilltype skilltype)
    {
        //TÄNNE TULEE SKILPOINTTIEN LOGIIKKA
        Skilltype skillrequirement = GetSkillRequirement(skilltype);
        if (skillrequirement != Skilltype.none)//and skillpoints 
        {
            if (isSkillUnlocked(skillrequirement) && GetSkillPoints()>=1)
            {
                Debug.Log("Skillpoints" + GetSkillPoints());
                SetSkillpoints(1);
                return true;
            }
            else
            {
                return false;
            }
        }
        else if (skillrequirement == Skilltype.none)
        {
            if ((skilltype == Skilltype.flames || skilltype == Skilltype.poison) && GetSkillPoints() >= 1) {

                SetSkillpoints(1);
                return true;
            }else if (skilltype == Skilltype.sword)
                return true;

            else return false;
        }
        else return false;
    }

    public Skilltype GetSkillRequirement(Skilltype skilltype)
    {
        switch (skilltype)
        {
            case Skilltype.blocklv2: return Skilltype.block;
            case Skilltype.blocklv3: return Skilltype.blocklv2;
            case Skilltype.swordaltlv2: return Skilltype.sword;
            case Skilltype.swordlv2: return Skilltype.sword;
            case Skilltype.swordlv3: return Skilltype.swordlv2;

            case Skilltype.flameslv2:return Skilltype.flames;
            case Skilltype.flameslv3: return Skilltype.flameslv2;

            case Skilltype.poisonlv2:return Skilltype.poison;
        }
        return Skilltype.none;
    }

    public bool TryUnlockSkill(Skilltype skilltype)
    {
        if (CanUnlock(skilltype))
        {
            UnlockSkill(skilltype);
            Debug.Log(skilltype);
            return true;
        }
        else
        {
            return false;
        }
    }

    public void LoadSkills(bool[] skillList)
    {
        FillSkillIndex();

        for(int i = 0; i < skillList.Length; i++)
        {
            if(skillList[i])
            {
                UnlockSkill(skillIndex[i]);
            }
        }
    }

    public void FillSkillIndex()
    {
        skillIndex.Add(0, Skilltype.sword);
        skillIndex.Add(1, Skilltype.swordlv2);
        skillIndex.Add(2, Skilltype.swordlv3);
        skillIndex.Add(3, Skilltype.swordaltlv2);
        skillIndex.Add(4, Skilltype.flames);
        skillIndex.Add(5, Skilltype.flameslv2);
        skillIndex.Add(6, Skilltype.flameslv3);
        skillIndex.Add(7, Skilltype.poison);
        skillIndex.Add(8, Skilltype.poisonlv2);
    }
}
