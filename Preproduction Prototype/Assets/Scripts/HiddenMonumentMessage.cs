    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HiddenMonumentMessage : MonoBehaviour
{
    public GameObject clueMessage;

    //let the player know there is a hidden monument nearby if they enter the radius
    private void OnTriggerEnter(Collider other)
    {
        clueMessage.SetActive(true);
    }

    //deactivate when they leave the radius
    private void OnTriggerExit(Collider other)
    {
        clueMessage.SetActive(false);
    }

    //if they open the monument deactivate the message
    private void Update()
    {
        if (HiddenMonument.endRadius == true)
        {
            clueMessage.SetActive(false);
        }
    }
}
