using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GPS_Location : MonoBehaviour
{
    private float longitude;
    private float latitude;

    
    public float testlongitude;

    public float testlatitude;

    private float attitude;
    [SerializeField]
    private Text gpsText;

    // Start is called before the first frame update
    void Start()
    {

        testlatitude = 55.867709f;
        testlongitude = -4.248789f;

        Input.gyro.enabled = true;      // Enable the use of the phones Gyroscope

        // If the app has location services enabled
        if (Input.location.isEnabledByUser)
        {
            StartCoroutine(GetLocation());      // Call the GetLocation function
        }
    }

    private IEnumerator GetLocation()
    {
        Input.location.Start();     // Begin using the phones location services

        // While the location services are initializing
        while (Input.location.status == LocationServiceStatus.Initializing)
        {
            yield return new WaitForSeconds(0.5f);      // Wait for half a second
        }
        latitude = Input.location.lastData.latitude;    // Set the latitude float to the value of the phones GPS latitude
        longitude = Input.location.lastData.longitude;  // Set the longitude float to the value of the phones GPS longitude
        yield break;    // End the function
    }

    // Update is called once per frame
    void Update()
    {

        latitude = Input.location.lastData.latitude;    // Set the latitude float to the value of the phones GPS latitude
        longitude = Input.location.lastData.longitude;  // Set the longitude float to the value of the phones GPS longitude
        attitude = Input.gyro.attitude.eulerAngles.z;   // Set the attitude float to the value of the phones gyroscopic rotation around the x axis
        gpsText.text = "Lat: " + latitude + "\nLon: " + longitude + "\nAtt: " + attitude;       // Display the latitude, longitude and attitude floats

    }

}
