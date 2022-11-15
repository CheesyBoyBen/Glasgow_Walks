using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField]
    public GPS_Location cvc;

    [Header("Map Markers")]
    [SerializeField]
    public MarkerScript topLeft;
    [SerializeField]
    public MarkerScript topRight;
    [SerializeField]
    public MarkerScript bottomLeft;
    [SerializeField]
    public MarkerScript bottomRight;



    float gpsXDist;
    float gpsZDist;
    float localXDist;
    float localZDist;
    float playerXDist;
    float playerZDist;
    float playerXPercent;
    float playerZPercent;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        gpsXDist = topRight.longitude - topLeft.longitude;      // Find the real world longitude distance between the top 2 markers
        gpsZDist = bottomLeft.latitude - topLeft.latitude;      // Find the real world latitude distance between the left 2 markers

        localXDist = topRight.transform.position.x - topLeft.transform.position.x;      // Find the in game x distance between the top 2 markers
        localZDist = bottomLeft.transform.position.z - topLeft.transform.position.z;    // Find the in game z distance between the left 2 markers

        playerXDist = cvc.testlongitude - topLeft.longitude;    // Find the real world longitude distance between the top left marker and the player
        playerZDist = cvc.testlatitude - topLeft.latitude;      // Find the real world latitude distance between the top left marker and the player

        playerXPercent = playerXDist / gpsXDist;       // Find the percentage of the full longitude distance the player distance represents
        playerZPercent = playerZDist / gpsZDist;       // Find the percentage of the full latitude distance the payer distance represents

        //Debug.Log(cvc.testlongitude);


        // Set the players in game position to the equivalent position to their real world position
        transform.position = new Vector3((localXDist * playerXPercent) + topLeft.transform.position.x, transform.position.y, (localZDist * playerZPercent) + topLeft.transform.position.z);


    }


    public void SwitchMovemntType()
    {
        Debug.Log("Hello");
    }
}
