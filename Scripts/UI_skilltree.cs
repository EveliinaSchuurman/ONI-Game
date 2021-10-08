using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_skilltree : MonoBehaviour
{
    private PlayerSkills playerSkills;

    
    [SerializeField] private SkillUnlockPath[] skillUnlockPathArray;
    public Text txt;
    public Text skillpointText;

    public void swordbuttonunlock()
    {
        playerSkills.TryUnlockSkill(PlayerSkills.Skilltype.sword);
        if (playerSkills.TryUnlockSkill(PlayerSkills.Skilltype.sword))
            txt.text = "Jutte unlocked!";
        else
            txt.text = "Not enough skillpoints or requirements not met";

        SetPlayerSkills(playerSkills);
    }
    public void swordlv2buttonunlock()
    {
        playerSkills.TryUnlockSkill(PlayerSkills.Skilltype.swordlv2);
        if (playerSkills.TryUnlockSkill(PlayerSkills.Skilltype.swordlv2))
            txt.text = "Washizaki unlocked!";
        else
            txt.text = "Not enough skillpoints or requirements not met";

        SetPlayerSkills(playerSkills);
    }
    public void swordaltlv2buttonunlock()
    {
        playerSkills.TryUnlockSkill(PlayerSkills.Skilltype.swordaltlv2);
        if (playerSkills.TryUnlockSkill(PlayerSkills.Skilltype.swordaltlv2))
            txt.text = "Kusarigama unlocked! This sword uses poison.";
        else
            txt.text = "Not enough skillpoints or requirements not met";

        SetPlayerSkills(playerSkills);
    }
    public void swordlv3buttonunlock()
    {
        playerSkills.TryUnlockSkill(PlayerSkills.Skilltype.swordlv3);
        if (playerSkills.TryUnlockSkill(PlayerSkills.Skilltype.swordlv3))
            txt.text = "Katana unlocked! Strongest sword now yours.";
        else
            txt.text = "Not enough skillpoints or requirements not met";

        SetPlayerSkills(playerSkills);
    }
    public void blockbuttonunlock()
    {
        playerSkills.TryUnlockSkill(PlayerSkills.Skilltype.block);
    }
    public void blocklv2buttonunlock()
    {
        playerSkills.TryUnlockSkill(PlayerSkills.Skilltype.blocklv2);
    }
    public void blocklv3buttonunlock()
    {
        playerSkills.TryUnlockSkill(PlayerSkills.Skilltype.blocklv3);
    }
    public void flamesbuttonunlock()
    {
        playerSkills.TryUnlockSkill(PlayerSkills.Skilltype.flames);
        if (playerSkills.TryUnlockSkill(PlayerSkills.Skilltype.flames))
            txt.text = "Flames unlocked!";
        else
            txt.text = "Not enough skillpoints or requirements not met";

        SetPlayerSkills(playerSkills);
    }
    public void flmaeslv2buttonunlock()
    {
        playerSkills.TryUnlockSkill(PlayerSkills.Skilltype.flameslv2);
        if (playerSkills.TryUnlockSkill(PlayerSkills.Skilltype.flameslv2))
            txt.text = "Incinerate unlocked! Enough firepower to blast a village.";
        else
            txt.text = "Not enough skillpoints or requirements not met";

        SetPlayerSkills(playerSkills);
    }
    public void flameslv3buttonunlock()
    {
        playerSkills.TryUnlockSkill(PlayerSkills.Skilltype.flameslv3);
        if (playerSkills.TryUnlockSkill(PlayerSkills.Skilltype.flameslv3))
            txt.text = "Mercy unlocked! The most powerful spell...";
        else
            txt.text = "Not enough skillpoints or requirements not met";

        SetPlayerSkills(playerSkills);
    }
    public void poisonbuttonunlock()
    {
        playerSkills.TryUnlockSkill(PlayerSkills.Skilltype.poison);
        if (playerSkills.TryUnlockSkill(PlayerSkills.Skilltype.poison))
            txt.text = "Kodoku unlocked!";
        else
            txt.text = "Not enough skillpoints or requirements not met";

        SetPlayerSkills(playerSkills);
    }
    public void poisonlv2buttonunlock()
    {
        playerSkills.TryUnlockSkill(PlayerSkills.Skilltype.poisonlv2);
        if (playerSkills.TryUnlockSkill(PlayerSkills.Skilltype.poisonlv2))
            txt.text = "Life steal unlocked! Impressive...";
        else
            txt.text = "Not enough skillpoints or requirements not met";

        SetPlayerSkills(playerSkills);
    }


    public void SetPlayerSkills(PlayerSkills playerSkills)
    {
        this.playerSkills = playerSkills;
        //Debug.Log("visuals updated");
        UpdateVisuals();
    }

    private void UpdateVisuals()
    {
        skillpointText.GetComponent<UnityEngine.UI.Text>().text = "Skillpoints: " + playerSkills.GetSkillPoints();
    }



    [Serializable]
    public class SkillUnlockPath
    {
        public PlayerSkills.Skilltype skilltype;
        public Image[] linkimageArray;

    }
}
