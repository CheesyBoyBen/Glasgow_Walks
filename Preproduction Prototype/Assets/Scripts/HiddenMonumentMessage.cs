    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HiddenMonumentMessage : MonoBehaviour
{
    public GameObject clueMessage;



    private void OnTriggerEnter(Collider other)
    {
        clueMessage.SetActive(true);        //let the player know there is a hidden monument nearby if they enter the radius
    }


    private void OnTriggerExit(Collider other)
    {
        clueMessage.SetActive(false);       //deactivate when they leave the radius
    }


    private void Update()
    {
        if (Minigame.gameComplete == true)
        {
            Destroy(gameObject);        //if they completed the minigame permantly remove the clue message
        }
    }
}
