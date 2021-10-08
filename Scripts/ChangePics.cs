using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class ChangePics : MonoBehaviour
{
    public GameObject SceneMarker01;
    public GameObject SceneMarker02;
    public GameObject SceneMarker03;
    public GameObject SceneImage01;
    public GameObject SceneImage02;
    public GameObject SceneImage03;

    private Animator anim1;
    private Animator anim2;
    private Animator anim3;
    public Button SceneChanger;
    public int count = 0;

    public void Start(){
        anim1 = SceneImage01.GetComponent<Animator>();
        anim2 = SceneImage02.GetComponent<Animator>();
        anim3 = SceneImage03.GetComponent<Animator>();
        SceneMarker01.SetActive(true);
        anim1.Play("PictureMover");
    }

    public void Update(){
        if(Input.GetButtonDown("Fire1")){
            count++;
        }
        if (Input.GetButtonDown("Fire1") && count == 1)
        {
            Debug.Log("Second if clause triggered");
            SceneMarker01.SetActive(false);
            SceneMarker02.SetActive(true);
            anim2.Play("PictureMover");
        }
        if (Input.GetButtonDown("Fire1") && count == 3)
        {
            Debug.Log("Third if clause triggered");
            SceneMarker02.SetActive(false);
            SceneMarker03.SetActive(true);
            anim3.Play("PictureMover");
        }
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(3);
    }
}
