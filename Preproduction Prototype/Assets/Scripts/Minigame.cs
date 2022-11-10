using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Minigame : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject text;
    public GameObject game;
    public static bool gameComplete = false;

   public void CorrectAnswer()
    {
        text.GetComponent<TextMeshProUGUI>().text = "Correct Answer";
        Gems.score += 10;
        StartCoroutine(CloseDelay());
        gameComplete = true;
    }

   public void WrongAnswer()
    {
        text.GetComponent<TextMeshProUGUI>().text = "Wrong Answer Guess Again";
    }

    IEnumerator CloseDelay()
    {
        yield return new WaitForSeconds(3);
        game.SetActive(false);

    }
}
