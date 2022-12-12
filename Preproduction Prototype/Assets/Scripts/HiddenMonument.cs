using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
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
    [SerializeField]
    private GameObject messageRadius;
    [SerializeField]
    private GameObject Gem;

    private bool imageActive = false;
    private bool inRange = false;
    private bool firstTime = true;

    [SerializeField]
    private Camera cam;



    private void OnTriggerEnter(Collider collider)
    {
       // Debug.Log("enter");
        inRange = true;
        monumentRend.SetActive(true);        //reveal the monument if the player approaches it since its meant to be hidden
        messageRadius.SetActive(false);
    }
    
    private void OnTriggerExit(Collider collider)
    {
        inRange = false;
        if (Minigame.gameComplete == true)      //if the player has already completed the minigame then keep the monument renderer active if they leave the area
        {
            monumentRend.SetActive(true);
        }
        else
        {
            messageRadius.SetActive(true);
            monumentRend.SetActive(false);       //if they have not then hide the monument when they leave
        }
    }


    void Update()
    {
       Gem.transform.Rotate(0, 0.2f, 0, Space.World);
        if (inRange == true)
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);        //get instance of touch
                Ray ray = cam.ScreenPointToRay(touch.position);     //get the touch position on the screen and project a ray
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))      //check if it hits a gameobject
                {
                    if(hit.collider.gameObject.CompareTag("Block Ray"))
                    {

                    }

                    else if (hit.collider.gameObject.CompareTag("HMonument"))     //if the gameobject has a "HMonument" tag then show the monument image
                    {
                        GameObject curHMonument = hit.collider.gameObject.GetComponent<HiddenMonument>().HMImage;
                        curHMonument.SetActive(true);
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
