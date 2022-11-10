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
    private bool trigger = false;
   
    static public bool facing = false;

    private void OnTriggerEnter(Collider other)
    {
        enterMessage.SetActive(true);
        entermessage = true;
        trigger = true;
    }

    private void OnTriggerExit(Collider other)
    {
        enterMessage.SetActive(false);
        trigger = false;
    }

    private void Update()
    {



        if (facing == true && Input.touchCount > 0 && trigger == true || Input.GetKeyDown("e") && trigger == true)
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


        if (Input.GetKeyDown(KeyCode.F) && monumentimage == true)
            {
                monumentImage.SetActive(false);
                //player.GetComponent<CharacterController>().enabled = true;
                firstInteract = false;
        }
        
    }

    public void closeImage()
    {
        if (monumentimage == true)
        {
            monumentImage.SetActive(false);
            //player.GetComponent<CharacterController>().enabled = true;
            firstInteract = false;
        }
    }

}

