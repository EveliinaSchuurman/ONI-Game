using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }
public class CombatManager : MonoBehaviour
{
    public GameObject dayplanner;
    public Image enemyImage;

    public GameObject chooseAction;
    public GameObject dialogue;
    public GameObject attackSelector;
    public GameObject infoBox;

    private GameObject latestBox;

    public Text textInfo;
    public Text textDialogue;

    public GameObject talkButton;
    public GameObject spareButton;

    public List<GameObject> skillDatabase;
    public List<GameObject> skillButtons;

    private List<Skill> enemySkillList = new List<Skill>();

    private Unit playerUnit = new Unit();
    private Unit enemyUnit = new Unit();

    private Player player;

    public BattleHud playerHUD;
    public BattleHud enemyHUD;

    public BattleState state;

    public float spareThreshold;
    private int dialogueOption = 0;
    private float turn;

    public Sprite[] enemySprites;

    public void SetupPlayer(Player __player)
    {
        player = __player;

        playerUnit.unitName = "Oni";
        playerUnit.unitLevel = 1;
        playerUnit.type = UnitType.PLAYER;
        playerUnit.maxHP = player.maxHP;
        playerUnit.currentHP = player.HP;
        playerUnit.poisonDuration = 0;
    }

    public void StartBattle(Player _player)
    {
        //SetupPlayer(_player);

        state = BattleState.START;
        StartCoroutine(SetupBattle());
    }

    IEnumerator SetupBattle()
    {
        turn = 1;

        playerHUD.SetHud(playerUnit);
        enemyHUD.SetHud(enemyUnit);

        HideBoxes();
        infoBox.SetActive(true);
        textInfo.text = "Enemy attacks!";

        yield return new WaitForSeconds(1.2f);

        HideBoxes();
        latestBox = chooseAction;
        latestBox.SetActive(true);

        state = BattleState.PLAYERTURN;
        EnemyTurn();
    }

    public void UpdateHUD()
    {
        enemyHUD.UpdateHP(enemyUnit.currentHP);
        playerHUD.UpdateHP(playerUnit.currentHP);
    }

    IEnumerator UnitAttack(Unit unit, Unit opponent, Skill skill)
    {
        HideBoxes();
        infoBox.SetActive(true);

        bool isOpponentDead = false;
        bool isUnitDead = false;

        textInfo.text = unit.unitName + " used " + skill.skillName;

        yield return new WaitForSeconds(1f);

        if (skill.skillType == SkillTypes.BASIC)
        {
            isOpponentDead = opponent.TakeDamage(skill.damage);
            UpdateHUD();
            yield return new WaitForSeconds(1f);
        } 
        else if (skill.skillType == SkillTypes.POWERCOST)
        {
            isOpponentDead = opponent.TakeDamage(skill.damage);
            UpdateHUD();
            yield return new WaitForSeconds(1f);
            isUnitDead = unit.TakeDamage(skill.powerCost);
            UpdateHUD();
            yield return new WaitForSeconds(1f);
        } 
        else if (skill.skillType == SkillTypes.LIFESTEAL)
        {
            isOpponentDead = opponent.TakeDamage(skill.damage);
            UpdateHUD();
            yield return new WaitForSeconds(1f);
            unit.Heal(skill.heal);
            UpdateHUD();
            yield return new WaitForSeconds(1f);
        } 
        else if (skill.skillType == SkillTypes.POISON)
        {
            opponent.poisonDuration = skill.poisonDuration;
            opponent.poisonDamage = skill.poisonDamage;

            if (opponent.type == UnitType.PLAYER)
                playerHUD.ShowPoison();
            else
                enemyHUD.ShowPoison();

            yield return new WaitForSeconds(1f);
        }
        else if (skill.skillType == SkillTypes.POISONSWORD)
        {
            isOpponentDead = opponent.TakeDamage(skill.damage);
            UpdateHUD();
            opponent.poisonDuration = skill.poisonDuration;
            opponent.poisonDamage = skill.poisonDamage;

            if (opponent.type == UnitType.PLAYER)
                playerHUD.ShowPoison();
            else
                enemyHUD.ShowPoison();

            yield return new WaitForSeconds(1f);
        }
        else if (skill.skillType == SkillTypes.MERCY)
        {
            if ((opponent.currentHP / opponent.maxHP <= 0.5f) && (opponent.type == UnitType.ASSASSIN || opponent.type == UnitType.BOSS))
            {
                opponent.currentHP = 1;
                UpdateHUD();
            } 
            else
            {
                HideBoxes();
                infoBox.SetActive(true);
                
                if (opponent.type == UnitType.DEMON)
                {
                    textInfo.text = "Cannot use Mercy on a demon!";
                } 
                else
                {
                    textInfo.text = "Cannot use Mercy on a healthy target!";
                }
            }
            yield return new WaitForSeconds(1f);
        }

        // Check poison
        if (opponent.poisonDuration > 0)
        {
            HideBoxes();
            infoBox.SetActive(true);

            textInfo.text = opponent.unitName + " took damage from the poison";

            isOpponentDead = opponent.TakeDamage(opponent.poisonDamage);
            opponent.poisonDuration--;
            UpdateHUD();

            if (opponent.poisonDuration == 0)
            {
                if (opponent.type == UnitType.PLAYER)
                    playerHUD.HidePoison();
                else
                    enemyHUD.HidePoison();
            }
        }

        yield return new WaitForSeconds(1f);

        if (isUnitDead)
        {
            if (unit.type == UnitType.PLAYER)
            {
                state = BattleState.LOST;
                StartCoroutine(EndBattle());
            } else
            {
                state = BattleState.WON;
                StartCoroutine(EndBattle());
            }
        } 
        else if (isOpponentDead)
        {
            if (unit.type == UnitType.PLAYER)
            {
                state = BattleState.WON;
                StartCoroutine(EndBattle());
            }
            else
            {
                state = BattleState.LOST;
                StartCoroutine(EndBattle());
            }
        }
        else
        {
            if (unit.type == UnitType.PLAYER)
            {
                state = BattleState.ENEMYTURN;
                EnemyTurn();
            }
            else
            {
                state = BattleState.PLAYERTURN;
                PlayerTurn();
            }
        }

        turn += 0.5f;
    }

    IEnumerator EndBattle()
    {
        HideBoxes();
        infoBox.SetActive(true);

        enemyHUD.HidePoison();
        playerHUD.HidePoison();

        if (state == BattleState.WON)
        {
            textInfo.text = "You won!";
            yield return new WaitForSeconds(1.0f);

            if (enemyUnit.type == UnitType.BOSS)
            {
                dayplanner.GetComponent<Dayplanner>().Death(3);
            } 
            else if (enemyUnit.type == UnitType.ASSASSIN)
            {
                player.HP = playerUnit.currentHP;
                dayplanner.GetComponent<Dayplanner>().UpdateSacrificeCount(true, false);
            } 
            else
            {
                player.HP = playerUnit.currentHP;
                dayplanner.GetComponent<Dayplanner>().UpdateSacrificeCount(false, true); // DEMONI
            }
        }
        else if (state == BattleState.LOST)
        {
            textInfo.text = "You lost...";
            yield return new WaitForSeconds(1.0f);

            if (enemyUnit.type == UnitType.BOSS)
            {
                dayplanner.GetComponent<Dayplanner>().Death(5);
            }
            else if (enemyUnit.type == UnitType.DEMON)
            {
                dayplanner.GetComponent<Dayplanner>().Death(2);
            }
            else
            {
                textInfo.text = "You are barely alive but manage to escape...";
                yield return new WaitForSeconds(1.0f);

                playerUnit.currentHP = 1;

                player.HP = playerUnit.currentHP;
                dayplanner.GetComponent<Dayplanner>().UpdateSacrificeCount(false, false);
            }
        }
    }

    void PlayerTurn()
    {
        HideBoxes();
        latestBox.SetActive(true);
        latestBox = attackSelector;
    }

    public void EnemyTurn()
    {
        // Choose skill
        StartCoroutine(UnitAttack(enemyUnit, playerUnit, ChooseSkill()));
    }

    // ENEMY CHOOSES SKILL TO USE
    public Skill ChooseSkill()
    {
        if (enemyUnit.type == UnitType.ASSASSIN || enemyUnit.type == UnitType.BOSS)
        {
            if (turn == 1)
                return enemySkillList[1]; // Assassination
            else if (playerUnit.poisonDuration == 0)
                return enemySkillList[2]; // Poison
            else
                return enemySkillList[0]; // Basic attack
        } 
        else
        {
            if (enemyUnit.currentHP / enemyUnit.maxHP > 0.5f)
                return enemySkillList[1]; // Flame
            else if (enemyUnit.currentHP / enemyUnit.maxHP < 0.3f)
                return enemySkillList[2]; // Life Steal
            else
                return enemySkillList[0]; // Basic attack
        }
    }

    // CHECK IF BATTLE STARTS
    public bool CheckIfBattle(Player player)
    {

        SetupPlayer(player);

        if (player.day == 21)
        {
            CreateEnemy(UnitType.BOSS);
            return true;
        }
        else if (player.day == 0)
        {
            CreateEnemy(UnitType.ASSASSIN);
            return true;
        } 
        else if (player.day % 2 == 0)
        {
            if (player.uncontentness < 1.5)
            {
                CreateEnemy(UnitType.DEMON);
                return true;
            }
            else
            {
                CreateEnemy(UnitType.ASSASSIN);
                return true;
            }
        }

        return false;
    }

    // CREATE ENEMY
    public void CreateEnemy(UnitType enemyType)
    {
        System.Random rnd = new System.Random();
        int rndNum;

        enemyUnit.unitDialogue.Clear();

        int HPscaling = (int)Mathf.Round((player.day / 2) * 10);

        if (enemyType == UnitType.BOSS)
        {
            enemyUnit.unitName = "Assassin Hikaru";
            enemyUnit.unitLevel = 1;
            enemyUnit.type = UnitType.BOSS;
            enemyUnit.maxHP = 150;
            enemyUnit.currentHP = enemyUnit.maxHP;
            enemyUnit.poisonDuration = 0;
            enemyUnit.unitSprite = enemySprites[6];
            enemyUnit.unitDialogue.Add("You monster...");
        } 
        else if (enemyType == UnitType.ASSASSIN) {
            enemyUnit.unitName = "Assassin";
            enemyUnit.unitLevel = 1;
            enemyUnit.type = UnitType.ASSASSIN;
            enemyUnit.maxHP = 30 + HPscaling;
            enemyUnit.currentHP = enemyUnit.maxHP;
            enemyUnit.poisonDuration = 0;
            rndNum = rnd.Next(0, 2);
            enemyUnit.unitSprite = enemySprites[rndNum];
            enemyUnit.unitDialogue.Add("Die, demon!");
        } 
        else
        {
            enemyUnit.unitName = "Demon";
            enemyUnit.unitLevel = 1;
            enemyUnit.type = UnitType.DEMON;
            enemyUnit.maxHP = 30 + HPscaling;
            enemyUnit.currentHP = enemyUnit.maxHP;
            enemyUnit.poisonDuration = 0;
            rndNum = rnd.Next(2, 6);
            enemyUnit.unitSprite = enemySprites[rndNum];
            enemyUnit.unitDialogue.Add("You're weak!");
        }

        enemyImage.GetComponent<Image>().sprite = enemyUnit.unitSprite;

        enemySkillList.Clear();

        if (enemyUnit.type == UnitType.ASSASSIN || enemyUnit.type == UnitType.BOSS)
        {
            enemySkillList.Add(skillDatabase[0].GetComponent<Skill>());
            enemySkillList.Add(skillDatabase[9].GetComponent<Skill>());
            enemySkillList.Add(skillDatabase[7].GetComponent<Skill>());
        } 
        else
        {
            enemySkillList.Add(skillDatabase[0].GetComponent<Skill>());
            enemySkillList.Add(skillDatabase[4].GetComponent<Skill>());
            enemySkillList.Add(skillDatabase[8].GetComponent<Skill>());
        }
    }

    // ---------- BUTTONS ---------- //

    public void OnAttackButton(Button button)
    {
        if (state != BattleState.PLAYERTURN)
            return;

        Skill skill = button.gameObject.GetComponent<CombatButtons>().skill.GetComponent<Skill>();
        StartCoroutine(UnitAttack(playerUnit, enemyUnit, skill));
    }

    public void ChooseAttack()
    {
        HideBoxes();
        attackSelector.SetActive(true);

        HideSkills();
    }

    // Skill buttons
    public void HideSkills()
    {
        bool[] skillBool = player.GetSkillList();

        for (int i = 0; i < 9; i++)
        {
            if (skillBool[i])
            {
                skillButtons[i].SetActive(true);
            }
            else
            {
                skillButtons[i].SetActive(false);
            }
        }
    }

    public void ChooseSpare()
    {
        if(enemyUnit.type == UnitType.BOSS)
        {
            dayplanner.GetComponent<Dayplanner>().Death(4);
        } else
        {
            StartCoroutine(SpareEnd());
        }
    }

    IEnumerator SpareEnd()
    {
        HideBoxes();
        infoBox.SetActive(true);

        spareButton.SetActive(false);
        talkButton.SetActive(true);

        enemyHUD.HidePoison();
        playerHUD.HidePoison();

        textInfo.text = "You chose to spare the human. Maybe the villagers will calm down...";

        yield return new WaitForSeconds(2.0f);

        player.HP = playerUnit.currentHP;
        dayplanner.GetComponent<Dayplanner>().UpdateSacrificeCount(false, false);
    }

    public void ChooseTalk()
    {
        HideBoxes();
        dialogue.SetActive(true);

        textDialogue.text = enemyUnit.unitDialogue[dialogueOption];

        if(dialogueOption + 1 < enemyUnit.unitDialogue.Count)
        {
            dialogueOption++;
        }
    }

    public void ShowChooseAction()
    {
        HideBoxes();
        chooseAction.SetActive(true);

        if ((enemyUnit.type == UnitType.ASSASSIN || enemyUnit.type == UnitType.BOSS) && (enemyUnit.currentHP / enemyUnit.maxHP <= spareThreshold))
        {
            spareButton.SetActive(true);
            talkButton.SetActive(false);
        }
        else
        {
            spareButton.SetActive(false);
            talkButton.SetActive(true);
        }
    }

    public void HideBoxes()
    {
        chooseAction.SetActive(false);
        attackSelector.SetActive(false);
        dialogue.SetActive(false);
        infoBox.SetActive(false);
    }
}
