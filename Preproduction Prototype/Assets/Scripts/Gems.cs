using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class Gems : MonoBehaviour
{

    public static int score;   
    public int score2;
    public GameObject scoreBox;

    public void Update()
    {
        //makes the local score = to the global static score and displays it
        score2 = score;
        scoreBox.GetComponent<TextMeshProUGUI>().text = "" + score2; 
    }
}
