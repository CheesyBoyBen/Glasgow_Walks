using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{

    [SerializeField]
    public MarkerScript topLeft;
    [SerializeField]
    public MarkerScript topRight;
    [SerializeField]
    public MarkerScript bottomLeft;
    [SerializeField]
    public MarkerScript bottomRight;
    [SerializeField]
    public GPS_Location cvc;

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
        gpsXDist = topRight.longitude - topLeft.longitude;
        gpsZDist = bottomLeft.latitude - topLeft.latitude;

        localXDist = topRight.transform.position.x - topLeft.transform.position.x;
        localZDist = bottomLeft.transform.position.z - topLeft.transform.position.z;

        playerXDist = cvc.testlongitude - topLeft.longitude;
        playerZDist = cvc.testlatitude - topLeft.latitude;

        playerXPercent = playerXDist / gpsXDist;
        playerZPercent = playerZDist / gpsZDist;



        transform.position = new Vector3((localXDist * playerXPercent) + topLeft.transform.position.x, transform.position.y, (localZDist * playerZPercent) + topLeft.transform.position.z);
    }

    void FindClosestMarkers()
    {

    }
}
