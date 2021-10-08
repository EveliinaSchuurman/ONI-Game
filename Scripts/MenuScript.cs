using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class MenuScript : MonoBehaviour
{
    
    public GameObject SceneImage03;

    public Animator anim1;
   
    public Button start;
    public Button Load;
    public Button Quit;

    public void Start()
    {
        anim1.Play("PictureMover");

        if (!SaveSystem.SaveExists())
        {
            Load.interactable = false;
        } 
        else
        {
            Load.interactable = true;
        }
    }
    
    public void Update()
    {
        if (anim1.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
        {
            SceneImage03.GetComponent<Animator>().StopPlayback();
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene(2);//check this
    }
    public void LoadButton()
    {
        Player player = GameObject.FindWithTag("Player").GetComponent<Player>();
        player.LoadPlayer();
        SceneManager.LoadScene(3);
    }
    public void QuitButton()
    {
        Application.Quit();
    }
}
