using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleHud : MonoBehaviour
{
    private float maxHP;
    private float currHP, currHPslow;
    private float timer = 0;

    public Text HP;
    public Image HPbar;
    public GameObject poisonImg;

    void Update()
    {
        if (currHPslow != currHP)
        {
            currHPslow = Mathf.Lerp(currHPslow, currHP, timer);
            timer += 0.1f * Time.deltaTime;

            SetHP();
        }
    }

    public void SetHud(Unit unit)
    {
        maxHP = unit.maxHP;
        currHP = unit.currentHP;
        currHPslow = unit.currentHP;

        SetHP();
    }

    public void SetHP()
    {

        HP.text = currHP + " / " + maxHP;
        HPbar.fillAmount = currHPslow / maxHP;
    }

    public void UpdateHP(float newHP)
    {
        currHP = newHP;
        timer = 0;
    }

    public void ShowPoison()
    {
        poisonImg.SetActive(true);
    }

    public void HidePoison()
    {
        poisonImg.SetActive(false);
    }
}
