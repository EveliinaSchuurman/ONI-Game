using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    private static string saveDirectory = "/saves/";
    private static string saveFile = "player.dat";

    public static void SavePlayer (Player player)
    {
        if (!Directory.Exists(Application.persistentDataPath + saveDirectory))
        {
            Directory.CreateDirectory(Application.persistentDataPath + saveDirectory);
        }
        
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + saveDirectory + saveFile;
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(player);

        formatter.Serialize(stream, data);
        stream.Close();

        Debug.Log("Game saved: HP is " + player.HP);
    }

    public static PlayerData LoadPlayer()
    {
        string path = Application.persistentDataPath + saveDirectory + saveFile;

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();

            Debug.Log("Game loaded: HP is " + data.HP);

            return data;
        } else
        {
            Debug.LogError("Save file not found in" + path);
            return null;
        }
    }

    public static void DeleteAllSaves()
    {
        DirectoryInfo directory = new DirectoryInfo(Application.persistentDataPath + saveDirectory);
        directory.Delete(true);
        Directory.CreateDirectory(Application.persistentDataPath + saveDirectory);

        Debug.Log("Save game deleted");
    }

    public static bool SaveExists()
    {
        string savePath = Application.persistentDataPath + saveDirectory + saveFile;
        return File.Exists(savePath);
    }
}

[System.Serializable]
public class PlayerData
{
    public float uncontentness;
    public float HP;
    public float maxHP;
    public int day;
    public int currentPlace;
    public int skillpoints;
    public bool skill0, skill1, skill2, skill3, skill4, skill5, skill6, skill7, skill8;

    public PlayerData (Player player)
    {
        uncontentness = player.uncontentness;
        HP = player.HP;
        maxHP = player.maxHP;
        day = player.day;
        currentPlace = player.GetPlace();
        skillpoints = player.GetSkillpoints();
        skill0 = player.GetSkillList()[0];
        skill1 = player.GetSkillList()[1];
        skill2 = player.GetSkillList()[2];
        skill3 = player.GetSkillList()[3];
        skill4 = player.GetSkillList()[4];
        skill5 = player.GetSkillList()[5];
        skill6 = player.GetSkillList()[6];
        skill7 = player.GetSkillList()[7];
        skill8 = player.GetSkillList()[8];
    }
}
