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
    public GameObject billboard;
    public GameObject cube;
    private bool entermessage = false;
    private bool monumentimage = false;
    private bool firstInteract = true;
    private bool inRange = false;
    public GameObject enterRadius;
    [SerializeField]
    private Camera cam;

    [Header("Map Markers")]
    [SerializeField]
    public MarkerScript topLeft;
    [SerializeField]
    public MarkerScript topRight;
    [SerializeField]
    public MarkerScript bottomLeft;
    [SerializeField]
    public MarkerScript bottomRight;

    [Header("Location")]
    public float latitude;
    public float longitude;
    

    static public bool facing = false;

    void Start()
    {
        setPosition();
        //setview();
    }

    private void FixedUpdate()
    {
        if (MonumentEnterRadius.trigger == true)
        {
            inRange = true;
            enterMessage.SetActive(true);
            entermessage = true;
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

    private void setPosition()
    {
        float gpsXDist;
        float gpsZDist;
        float localXDist;
        float localZDist;
        float playerXDist;
        float playerZDist;
        float playerXPercent;
        float playerZPercent;


        gpsXDist = topRight.longitude - topLeft.longitude;      // Find the real world longitude distance between the top 2 markers
        gpsZDist = bottomLeft.latitude - topLeft.latitude;      // Find the real world latitude distance between the left 2 markers

        localXDist = topRight.transform.position.x - topLeft.transform.position.x;      // Find the in game x distance between the top 2 markers
        localZDist = bottomLeft.transform.position.z - topLeft.transform.position.z;    // Find the in game z distance between the left 2 markers

        playerXDist = longitude - topLeft.longitude;    // Find the real world longitude distance between the top left marker and the player
        playerZDist = latitude - topLeft.latitude;      // Find the real world latitude distance between the top left marker and the player

        playerXPercent = playerXDist / gpsXDist;       // Find the percentage of the full longitude distance the player distance represents
        playerZPercent = playerZDist / gpsZDist;       // Find the percentage of the full latitude distance the payer distance represents


        // Set the players in game position to the equivalent position to their real world position
        transform.position = new Vector3((localXDist * playerXPercent) + topLeft.transform.position.x, transform.position.y, (localZDist * playerZPercent) + topLeft.transform.position.z);
    }

    private void setview()
    {
        if (cube.activeSelf)
        {
            cube.SetActive(false);
            billboard.SetActive(true);
        }
        else
        {
            cube.SetActive(true);
            billboard.SetActive(false);
        }
    }
}


