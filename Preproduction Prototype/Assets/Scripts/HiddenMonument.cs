using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HiddenMonument : MonoBehaviour
{
    [SerializeField]
    private GameObject HMImage;
    public GameObject miniGame;

    [SerializeField]
    private GameObject monumentRend;
    [SerializeField]
    private BoxCollider enterRadius;

    private bool imageActive = false;
    private bool inRange = false;
    private bool firstTime = true;

    [SerializeField]
    private Camera cam;



    private void OnTriggerEnter(Collider enterRadius)
    {
        inRange = true;
        monumentRend.SetActive(true);        //reveal the monument if the player approaches it since its meant to be hidden
    }

    private void OnTriggerExit(Collider enterRadius)
    {
        inRange = false;
        if (Minigame.gameComplete == true)      //if the player has already completed the minigame then keep the monument renderer active if they leave the area
        {
            monumentRend.SetActive(true);
        }
        else
        {
            monumentRend.SetActive(false);       //if they have not then hide the monument when they leave
        }
    }


    void Update()
    {
        if (inRange == true)
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);        //get instance of touch
                Ray ray = cam.ScreenPointToRay(touch.position);     //get the touch position on the screen and project a ray
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))      //check if it hits a gameobject
                {
                    if (hit.collider.gameObject.CompareTag("HMonument"))     //if the gameobject has a "HMonument" tag then show the monument image
                    {
                        HMImage.SetActive(true);
                        //imageActive = true;
                        if (firstTime == true)       //if its the players first time then start the minigame and set firsttime to false so they can interact with the monument without activting the minigame
                        {
                            miniGame.SetActive(true);
                            firstTime = false;
                        }
                    }
                }
            }
    }

    public void CloseImage()
    {
        HMImage.SetActive(false);      //close the image if they press the close button
    }
}
