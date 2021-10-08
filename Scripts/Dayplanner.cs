using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;
using System;

public class Dayplanner:MonoBehaviour
{
    public Text deathTxt;
    public string deathString;
    public Text DialogueBoxRoom;
    public string dialogueText;
    public Text DialogueBoxRoad;
    public Text SkillText;

    public GameObject SkillTree; //all the canvases
    public GameObject MainRoad;
    public GameObject Battle;
    public GameObject Deaths;
    public GameObject BedRoom;
    public GameObject combatOverlay;

    public Image BedRoomDay;
    public Image BedroomNight;

    public Image SacrificeFigure;

    public Image MobLynch;
    public Image DemonMassacre;
    public Image OniLosesHisMind;
    public Image HappyEnding;
    public Image InsaneEnding;

    float Rice = 0.0f;

    public GameObject Scroll;
    public Button TryAgainButton;
    public Button EatBtn;
    public Button SacrificeBtnYes;
    public Button SacrificeBtnNo;
    public Button GoToMainRoadBtn;
    public Button LookAtStarsBtn;
    public Button GoToMainMenu;
    public Player player;
    public MusicScript musicScript;

    public Slider mainSlider;
    public float riceAmount;

    public void Start()
    {
        //eti pelaaja, laita päivä jne
        //DialogueBoxRoom.text = dialogueText;
        //mainSlider.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        musicScript = GameObject.FindWithTag("Music").GetComponent<MusicScript>();
        SkillTree.SetActive(false);
        MainRoad.SetActive(false);
        Battle.SetActive(false);
        Deaths.SetActive(false);
        BedRoom.SetActive(false);
        Morning();
}

    public void HandleDropDown(int val)
    {
        
        Rice = val + 1;

    }

    private void ToLynchOrNotToLynch()
    {
        if(player.uncontentness > player.day + 4)
        {
            Death(1);
        }
    }
    public void SacrificeYes()
    {
        UpdateUncontentness(Rice, true);
        ToLynchOrNotToLynch();
        MainRoad.SetActive(false);
        PlaceChanger(Player.Place.own_room);
        //ANIMAATIO
        Night();
    }
    public void SacrificeNO()
    {
        UpdateUncontentness(Rice, false);
        MainRoad.SetActive(false);
        PlaceChanger(Player.Place.own_room);
        //ANIMAATIO
        Night();
    }
    public void UpdateUncontentness(float rice, bool Uhri)
    {
        if (Uhri)
        {
            player.uncontentness += 1 + RiceDecider(rice);
            player.GetPlayerSkills().SetSkillpoints(-1);

        }
        else
            player.uncontentness += RiceDecider(rice);
        
        player.HP += rice * 5;

        if (player.HP > player.maxHP)
            player.HP = player.maxHP;

    }
    public void UpdateSacrificeCount(bool Uhri, bool Demoni)
    {
        if (Uhri && !Demoni)
        {
            player.uncontentness += 0.7f;
            player.GetPlayerSkills().SetSkillpoints(-1);
            Morning();
        }
        else if (!Uhri && !Demoni)
        {
            player.uncontentness -= 0.8f;
            Morning();

        }
        //
        else { // demoni
            player.maxHP += 20;
            player.GetPlayerSkills().SetSkillpoints(-1);
            Morning();
            
        } 

    }
    
    

    public float RiceDecider(float rice)
    {
        float multi = 3.0f;
        float uncontentAMount = 0;
        if (rice == 1 || rice == 0)
            uncontentAMount = 0.015f * multi;
        else if (rice == 2)
            uncontentAMount = 0.0155f * multi;
        else if (rice == 3)
            uncontentAMount = 0.0157f * multi;
        else if (rice == 4)
            uncontentAMount = 0.016f * multi;
        else if (rice == 5)
            uncontentAMount = 0.0172f * multi;
        else if (rice == 6)
            uncontentAMount = 0.02f * multi;
        else if (rice == 7)
            uncontentAMount = 0.0238f * multi;
        else if (rice == 8)
            uncontentAMount = 0.0312f * multi;
        else if (rice == 9)
            uncontentAMount = 0.0555f * multi;
        else if (rice == 10)
            uncontentAMount = 0.1f * multi;
        //tänne laskuja siitä kuinka paljon syödään riisiä tai ihmisiä
        //ricedecider
        return uncontentAMount;
    }
    public void EatButton()
    {
        if (SacrificeDay())
        {
            ToLynchOrNotToLynch();
            SacrificeFigure.gameObject.SetActive(true);
            DialogueBoxRoad.text = "A youngster with beautiful looks and soft " +
                "flesh has taken your interest. Eating humans is part of demons nature. Will you order the young one to come with you?";
            SacrificeBtnYes.gameObject.SetActive(true);
            SacrificeBtnNo.gameObject.SetActive(true);
            EatBtn.gameObject.SetActive(false);

        }
        else
        {
            ToLynchOrNotToLynch();
            UpdateUncontentness(Rice, false);
            MainRoad.SetActive(false);
            
            PlaceChanger(Player.Place.own_room);
            //ANIMAATIO
            BedRoom.SetActive(true);
            Night();
            
        }
    }

    public bool SacrificeDay()
    {
        if (player.day%2==0)
        {
            return true;
        }
        if (player.day % 2 == 1)
        {
            return false;
        }
        return false;
    }

    public void LookAtStars()
    {
        BedRoom.SetActive(false);
        SkillTree.gameObject.SetActive(true);
    }
    public void GoToSleep()
    {
        if (combatOverlay.GetComponent<CombatManager>().CheckIfBattle(player))
        {
            musicScript.PlayTrack2();
            Battle.SetActive(true);
            BedRoom.SetActive(false);
            combatOverlay.GetComponent<CombatManager>().StartBattle(player);
            player.day++;
        } 
        else
        {
            player.day++;
            Morning();
        }
    }

    public int GreetingDecider()
    {
        float z = (player.CheckUnContentness() + player.day);
        if (player.day == 0)
            return 0;
        else if (z < (player.day + 1))
        {
            return (player.day + 20);
        }
        else if (z <= (player.day + 2))
        {
            return (player.day);
        }
        else //(z <= (player.day + 4))
        {
            return (player.day + 40);
        }
    }

    public void PlaceChanger(Player.Place place)
    {
        player.currentPlace = place;
    }

    public void GoToMainRoad()
    {
        PlaceChanger(Player.Place.main_road);
        MainRoad.SetActive(true);
        SacrificeBtnNo.gameObject.SetActive(false);
        SacrificeBtnYes.gameObject.SetActive(false);
        SacrificeFigure.gameObject.SetActive(false);
        EatBtn.gameObject.SetActive(true);
        BedRoom.SetActive(false);
        DialogueBoxRoad.text = "Master, the people are ready to give you their food. How much will you take? Eating replenishes your body and soul, but please consider our wellbeing too.";
    }

    public void Morning()
    {
        musicScript.PlayTrack1();
        if(player.day != 0)
        {
            player.SavePlayer();
        }
        SkillTree.SetActive(false);
        MainRoad.SetActive(false);
        Battle.SetActive(false);
        Deaths.SetActive(false);

        BedRoom.SetActive(true);
        BedRoomDay.gameObject.SetActive(true);
        BedroomNight.gameObject.SetActive(false);
        player.currentPlace = Player.Place.own_room;
        DialogueBoxRoom.text = player.Greetings[GreetingDecider()];
        GoToMainRoadBtn.gameObject.SetActive(true);
    }

    public void Night()
    {
        SkillTree.SetActive(false);
        MainRoad.SetActive(false);
        Battle.SetActive(false);
        Deaths.SetActive(false);

        BedRoom.SetActive(true);
        BedRoomDay.gameObject.SetActive(false);
        BedroomNight.gameObject.SetActive(true);
        LookAtStarsBtn.gameObject.SetActive(true);
    }

    public void Death(int ReasonToDie)
    {
        Battle.SetActive(false);
        Deaths.SetActive(true);
        SaveSystem.DeleteAllSaves();
        TryAgainButton.gameObject.SetActive(true);
        switch (ReasonToDie)
        {

            case 1: // mob lynch
                MobLynch.gameObject.SetActive(true);
                deathTxt.text = "Too accustomed to his position above all his subjects, the demon took too much food and too many sacrifices. The villagers could take it no more, and so the demon was killed. But with no leader, the village soon succumbed to disputes between the people.";
                break;

            case 2: // demon lynch
                DemonMassacre.gameObject.SetActive(true);
                deathTxt.text = "Too weak to keep his position, the Master was killed by a more powerful demon. Rather than take his place, the foe destroyed the village and ate all living beings he saw. ";
                break;

            case 3: // oni lynch, Assassin is dead
                OniLosesHisMind.gameObject.SetActive(true);
                deathTxt.text = " Too long had he lived like a mortal, a mere peasant with the people. He craved power more than the good of the people, and in his hunger for strength went mad. The village became a ghost town, never to be inhabited again.";
                break;
            case 4: // Happy ending! 
                HappyEnding.gameObject.SetActive(true);
                TryAgainButton.gameObject.SetActive(false);
                GoToMainMenu.gameObject.SetActive(true);
                deathTxt.text = "Over the years the demon grew bored, and begun to dream of leaving. He was not fond of humans, but he had watched over them for so long. He didn't want it to end like this. A new ruler was needed. The newcomer was a perfect ruler. Now if she didn't stab him so damn deep, moving would be easier.";
                break;
            case 5: // Oni dies in final battle
                InsaneEnding.gameObject.SetActive(true);
                deathTxt.text = "The demon barely escaped the assassin and knew he needed more power before other demons arrive to take his place. His hunger grew as he ate until no one was left alive.";
                break;


            default:
                deathTxt.text = "you shouldn't really be seeing this text";
                break;
        }
    }

    public void TryAgain()
    {
        SceneManager.LoadScene(1); //MENU
    }

}