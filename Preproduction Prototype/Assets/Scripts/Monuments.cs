using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.IO;
//using UnityEngine.InputSystem.HID;
using UnityEditor;

public class Monuments : MonoBehaviour
{
    public GameObject player;
    public GameObject enterMessage;
    public GameObject monumentImage;
    private bool entermessage = false;
    private bool monumentimage = false;
    private bool firstInteract = true;
    private bool inRange = false;
    public GameObject enterRadius;
    [SerializeField]
    private Camera cam;

    static public bool facing = false;


    private void FixedUpdate()
    {
        if (MonumentEnterRadius.trigger == true)
        {
            inRange = true;
            enterMessage.SetActive(true);
            entermessage = true;
            Debug.Log("enter");

        }
        else
        {
            enterMessage.SetActive(false);
            inRange = false;
        }


        //if (facing == true && trigger == true || Input.GetKeyDown("e") && trigger == true)
        if (inRange == true)
        {

            if (Input.touchCount > 0)
            {
                enterRadius.GetComponent<SphereCollider>().enabled = false;
                Debug.Log("touching");
                Touch touch = Input.GetTouch(0);
                Ray ray = cam.ScreenPointToRay(touch.position);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.gameObject.CompareTag("Monument"))
                    {
                        monumentImage.SetActive(true);
                        monumentimage = true;
                        enterMessage.SetActive(false);
                        //player.GetComponent<CharacterController>().enabled = false;
                        if (firstInteract == true)
                        {
                            Gems.score += 5;
                        }
                    }
                }
            }

        }
        if (Input.GetKeyDown(KeyCode.F) && monumentimage == true)
        {
            monumentImage.SetActive(false);
            player.GetComponent<CharacterController>().enabled = true;
            firstInteract = false;
        }


    }
    public void closeImage()
    {
        if (monumentimage == true)
        {
            monumentImage.SetActive(false);
            player.GetComponent<CharacterController>().enabled = true;
            firstInteract = false;
        }
    }
}


