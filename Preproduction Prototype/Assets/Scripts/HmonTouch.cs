using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HmonTouch : MonoBehaviour
{
    public static bool trigger2;



    private void OnTriggerEnter(Collider other)
    {
        trigger2 = true;
    }

    private void OnTriggerExit(Collider other)
    {
        trigger2 = false;
    }
    void Update()
    {

    }
}
