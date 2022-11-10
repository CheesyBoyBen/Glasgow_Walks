using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HiddenMonument : MonoBehaviour
{
    public GameObject player;
    public GameObject HMonumentImage;
    public GameObject miniGame;
    public GameObject monumentRend;
    private bool active = false;
    public static bool endRadius = false;
    private bool firstTime = true;
    private bool trigger = false;

    static public bool facing = false;


    void Update()
    {
        // if the player is facing the monument and taps the screen while in the radius or presses e while in the radius activate the hidden monument
        if ((facing == true && Input.touchCount > 0 && trigger == true) || (Input.GetKeyDown("e") && trigger == true))
        {

            //player.GetComponent<CharacterController>().enabled = false; // locks the player down
            active = true; // true while the player is in the monument
            endRadius = false; //deactivate the clue message
            HMonumentImage.SetActive(true); 
            if (firstTime == true)
            {
                miniGame.SetActive(true); // if its the players first time then activate the minigame
            }
        }

        //close the monument screen if its active and the player has completed the minigame, set first time to false so the player cannot repeat the minigame
        if (Input.GetKeyUp(KeyCode.F) && active == true  && trigger == true && Minigame.gameComplete == true)
        {
            HMonumentImage.SetActive(false);
            //player.GetComponent<CharacterController>().enabled = true;
            firstTime = false;
        }

    }


    //activate the monument render if the player walks right up to it
    private void OnTriggerEnter(Collider other)
    {
        trigger = true;
        monumentRend.GetComponent<MeshRenderer>().enabled = true;
    }

    //deactivate the monument render if the player walks away unless they have completed the minigame then keep it permantly rendered
    private void OnTriggerExit(Collider other)
    {
        trigger = false;
        if(Minigame.gameComplete == true)
        {
            monumentRend.GetComponent<MeshRenderer>().enabled = true;
        }
        else
        {
            monumentRend.GetComponent<MeshRenderer>().enabled = false;
        }
    }

    //close the monument screen if the button is pressed
    public void closeImage()
    {
        if (HMonumentImage == true)
        {
            HMonumentImage.SetActive(false);
           // player.GetComponent<CharacterController>().enabled = true;
            firstTime = false;
        }
    }
}