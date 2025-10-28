using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    public GameObject manPlayer;
    public GameObject womanPlayer;
    public bool isMan;

    public void SetCharacter()
    {
        if (isMan == true)
        {
            manPlayer.tag = "Player";
            manPlayer.AddComponent<PlayerController>();
        }
        else if(isMan != true)
        {
            womanPlayer.tag = "Player";
            womanPlayer.AddComponent<PlayerController>();
        }
    }

}
