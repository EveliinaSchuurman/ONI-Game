using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private PlayerSkills playerSkills;
    public float uncontentness;
    public float HP;
    public float maxHP;
    public int day = 0;
    public Place currentPlace;
    public bool[] skillBool = new bool[9];

    public float CheckUnContentness()
    {
        return uncontentness;
    }
    public enum Place
    {
        own_room,
        main_road

    }

    public string[] Greetings = { 
        //neutrals
        "Good Morning Master! Another day as a ruler right? Haha. " +
            "Did you look at the stars yesterday night? If you didn't, you should tonight. Worth seeing. " +
            "Would you like to start your morning by collecting your breakfast? The villagers have gathered everything they have for you as usual.", 
        "Morning Master, I heard the villager elder has fallen sick. How unfortunate. Ready for breakfast?",
        "Morning Master. Ready for breakfast?",
        "Morning Master. Ready for breakfast?",
        "Morning Master, the villagers are a bit restless...",
        "Morning Master. What a beautiful day it is.",
        "Morning Master, sleep well?",
        "Morning Master.",
        "Morning Master, ready for breakfast?",
        "Morning Master.",
        "Morning Master, the villagers were talking of someone new moving into the house on the border.",
        "Morning Master, the villagers are a bit restless...",
        "Morning Master, there was some fighting at the town square yesterday.",
        "Morning Master, The new lady is very inquisitive, I must say.",
        "Morning Master, ready to wake up?",
        "Morning Master. What a beautiful day it is.",
        "Morning Master, I heard there was an uprising against a horrible lord up north a few weeks ago.",
        "Morning Master, the villagers are a bit restless...",
        "Morning Master, have you considered having an audience with the new lady? She's very demanding.",
        "Morning Master. has life in our small village ever bored you?",
        "Morning Master. Do you ever wonder what moving on is like?",

        //goods
        "Good Morning Master, I heard the villager elder has fallen sick. How unfortunate. Ready for breakfast?",
        "Good Morning Master, what a good weather we have!",
        "Good Morning Master, did you sleep well?",
        "Good Morning Master, ready to get some breakfast?",
        "Good Morning Master!",
        "Good Morning Master, we are closing in on autumn.",
        "Good Morning Master, are you ready to wake up?",
        "Good Morning Master!",
        "Good Morning Master, how about a walk around town today?",
        "Good Morning Master, I heard we have a new villager! How exciting. Ready for breakfast?",
        "Good Morning Master!",
        "Good Morning Master, how was your night?",
        "Good Morning Master! The new lady is very inquisitive, she must love our village!",
        "Good Morning Master, did you notice it's soon time for harvest?",
        "Good Morning Master!",
        "Good Morning Master, our kind and peaceful protector! Ready for some breakfast?",
        "Good Morning Master, have you heard that the new lady would like to meet you?",
        "Good Morning Master, how was your night?",
        "Good Morning Master, have you considered having an audience with the new lady? She really wants to meet you alone.",
        "Good Morning Master, has life in our small village ever bored you?",
        "Good Morning Master, have you ever thought of what the world is like outside?",
        
        //bad
        "Master, the villagers are a bit restless... Please know that the food they prapare is their own",
        "Master.",
        "Master, the villagers know the youngster you took didn't return home. We know you need power to protect us, but please be considerate",
        "Master, we will always be grateful that you helped us. So we hope you have mercy.",
        "Master.",
        "Master, the people are hungry.",//
        "Master, the villagers are a bit restless... Please know that the food they prapare is their own",
        "Master, the villagers know the youngster you took didn't return home. We know you need power to protect us, but please be considerate",
        "Master. we will always be grateful that you helped us. So we hope you have mercy.",
        "Master, may I have a day off? My child is feeling sick...",
        "Master.",
        "Master, a new villager has moved here. Please show her mercy",//
        "Master, the villagers are a bit restless... Please know that the food they prapare is their own",
        "Master, the new villager has been asking things around.. thought you should know.",
        "Master, the villagers know the youngster you took didn't return home. We know you need power to protect us, but please be considerate",
        "Master, we will always be grateful that you helped us. So we hope you have mercy.",
        "Master, the people are hungry.",//
        "Master.",
        "Master, the people grow desperate.",
        "Master, the villagers know the youngster you took didn't return home. We know you need power to protect us, but please be considerate",
        "Master, I'm afraid you don't have what it takes to be a leader anymore"};

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        playerSkills = new PlayerSkills();
        //get skillpoints

        SceneManager.LoadScene("Menu");
    }

    public PlayerSkills GetPlayerSkills()
    {
        return playerSkills;
    }

    public int GetPlace()
    {
        if (currentPlace == Place.own_room)
            return 0;
        else
            return 1;
    }

    public Place SetPlace(int num)
    {
        if (num == 0)
            return Place.own_room;
        else
            return Place.main_road;
    }

    public int GetSkillpoints()
    {
        return playerSkills.GetSkillPoints();
    }

    public bool[] GetSkillList()
    {
        skillBool[0] = CanUseSword();
        skillBool[1] = CanUseSwordlv2();
        skillBool[2] = CanUseSwordlv3();
        skillBool[3] = CanUseSwordAltlv2();
        skillBool[4] = CanUseFlames();
        skillBool[5] = CanUseFlameslv2();
        skillBool[6] = CanUseFlameslv3();
        skillBool[7] = CanUsePoison();
        skillBool[8] = CanUsePoisonlv2();

        return skillBool;
    }

    public bool CanUseSword()
    {
        return true;
    }
    public bool CanUseSwordlv2()
    {
        return playerSkills.isSkillUnlocked(PlayerSkills.Skilltype.swordlv2);
    }
    public bool CanUseSwordlv3()
    {
        return playerSkills.isSkillUnlocked(PlayerSkills.Skilltype.swordlv3);
    }
    public bool CanUseSwordAltlv2()
    {
        return playerSkills.isSkillUnlocked(PlayerSkills.Skilltype.swordaltlv2);
    }
    public bool CanUseBlock()
    {
        return playerSkills.isSkillUnlocked(PlayerSkills.Skilltype.block);
    }
    public bool CanUseBlocklv2()
    {
        return playerSkills.isSkillUnlocked(PlayerSkills.Skilltype.blocklv2);
    }
    public bool CanUseBlocklv3()
    {
        return playerSkills.isSkillUnlocked(PlayerSkills.Skilltype.blocklv3);
    }
    public bool CanUseFlames()
    {
        return playerSkills.isSkillUnlocked(PlayerSkills.Skilltype.flames);
    }
    public bool CanUseFlameslv2()
    {
        return playerSkills.isSkillUnlocked(PlayerSkills.Skilltype.flameslv2);
    }
    public bool CanUseFlameslv3()
    {
        return playerSkills.isSkillUnlocked(PlayerSkills.Skilltype.flameslv3);
    }
    public bool CanUsePoison()
    {
        return playerSkills.isSkillUnlocked(PlayerSkills.Skilltype.poison);
    }public bool CanUsePoisonlv2()
    {
        return playerSkills.isSkillUnlocked(PlayerSkills.Skilltype.poisonlv2);
    }

    public void SavePlayer()
    {
        SaveSystem.SavePlayer(this);
    }

    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();

        uncontentness = data.uncontentness;
        HP = data.HP;
        maxHP = data.maxHP;
        day = data.day;
        currentPlace = SetPlace(data.currentPlace);
        playerSkills.LoadSkillpoints(data.skillpoints);
        skillBool[0] = data.skill0;
        skillBool[1] = data.skill1;
        skillBool[2] = data.skill2;
        skillBool[3] = data.skill3;
        skillBool[4] = data.skill4;
        skillBool[5] = data.skill5;
        skillBool[6] = data.skill6;
        skillBool[7] = data.skill7;
        skillBool[8] = data.skill8;
        playerSkills.LoadSkills(skillBool);

    }

}

