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
    private bool InRange = false;
    [SerializeField]
    private GameObject enterRadius;
    private bool inRange;
    [SerializeField]
    private Camera cam;



    void Update()
    {

        if (HmonTouch.trigger2 == true)
        {
            inRange = true;
            monumentRend.GetComponent<MeshRenderer>().enabled = true;
        }
        else
        {
            inRange = false;
            if (Minigame.gameComplete == true)
            {
                monumentRend.GetComponent<MeshRenderer>().enabled = true;
            }
            else
            {
                monumentRend.GetComponent<MeshRenderer>().enabled = false;
            }
        }


        if (inRange == true)
        {

            if (Input.touchCount > 0)
            {
                enterRadius.GetComponent<BoxCollider>().enabled = false;
                Debug.Log("touching");
                Touch touch = Input.GetTouch(0);
                Ray ray = cam.ScreenPointToRay(touch.position);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    Debug.Log("hit");
                    if (hit.collider.gameObject.CompareTag("HMonument"))
                    {
                        Debug.Log("hitobject");
                        HMonumentImage.SetActive(true);
                        active = true;
                        // player.GetComponent<CharacterController>().enabled = false;
                        if (firstTime == true)
                        {
                            miniGame.SetActive(true);
                        }
                    }
                }
            }

        }
        if (Input.GetKeyUp(KeyCode.F) && active == true && inRange == true && Minigame.gameComplete == true)
        {
            HMonumentImage.SetActive(false);
            player.GetComponent<CharacterController>().enabled = true;
            firstTime = false;
        }

    }

    public void closeImage()
    {
        if (HMonumentImage == true)
        {
            HMonumentImage.SetActive(false);
            player.GetComponent<CharacterController>().enabled = true;
            firstTime = false;
        }
    }
}
