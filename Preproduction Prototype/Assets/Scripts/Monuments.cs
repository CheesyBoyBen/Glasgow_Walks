using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.IO;
//using UnityEngine.InputSystem.HID;
using UnityEditor;

public class Monuments : MonoBehaviour
{
    public GameObject monumentImage;

    [SerializeField]
    private SphereCollider enterRadius;
    private GameObject billboard;
    private GameObject cube;
    public GameObject liveCam;

    [SerializeField]
  //  private GameObject[] Monumentslist;
    private bool firstInteract = true;
    private bool inrange = false;
    private bool monumentimage = false;

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

        billboard = transform.Find("Billboard").gameObject;
        cube = transform.Find("Cube").gameObject;
    }

    private void OnTriggerEnter(Collider enterRadius)
    {
        inrange = true;         // allows player to interact with monument
    }
    private void OnTriggerExit(Collider enterRadius)
    {
        inrange = false;
    }



    // Update is called once per frame
    void FixedUpdate()
    {
        //if (inrange == true)
        //{
        if (Input.touchCount > 0)
        {
            enterRadius.enabled = false;        //disables collider blocking player form touching monument
            Touch touch = Input.GetTouch(0);    //get instance of touch
            Ray ray = cam.ScreenPointToRay(touch.position);     //get players touch screen position and project a ray
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))      //if player hits an object check it
            {
                if (hit.collider.gameObject.CompareTag("Monument"))      //if it hits a monument with the "Monument" tag show monument image
                {
                    GameObject curMonumentImage = hit.collider.gameObject.GetComponent<Monuments>().monumentImage;
                    curMonumentImage.SetActive(true);

                    billboard = hit.collider.gameObject.GetComponent<Monuments>().billboard;
                    cube = hit.collider.gameObject.GetComponent<Monuments>().cube;

                    setview();


                    if (firstInteract)      //if its the players first time give them points and set first time to false so they can still interact with monument with getting points
                    {
                        Gems.score += 5;
                        firstInteract = false;
                        //setview();
                    }
                //}
            }
            }
        }
    }

    public void CloseImage()
    {
        monumentImage.SetActive(false);   //turn off the monument image if the player presses the button

        liveCam.gameObject.GetComponent<LiveCameraComtroller>().SetActive();
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
            Debug.Log("Hello");
        }

        
    }
}


