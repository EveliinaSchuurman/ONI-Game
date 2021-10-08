using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private UI_skilltree uI_Skilltree;

    public void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        uI_Skilltree.SetPlayerSkills(player.GetPlayerSkills());
    }
    public void Update()
    {
        uI_Skilltree.SetPlayerSkills(player.GetPlayerSkills());
    }
}
