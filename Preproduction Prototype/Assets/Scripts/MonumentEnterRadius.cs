using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonumentEnterRadius : MonoBehaviour
{
    public static bool trigger;



    private void OnTriggerEnter(Collider other)
    {
        trigger = true;
    }

    private void OnTriggerExit(Collider other)
    {
        trigger = false;
    }
    void Update()
    {
        
    }
}
