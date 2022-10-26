using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GPS_Location : MonoBehaviour
{
    public float longitude;
    public float latitude;
    public float attitude;
    public Text gpsText;

    // Start is called before the first frame update
    void Start()
    {
        Input.gyro.enabled = true;

        if (Input.location.isEnabledByUser)
        {
            StartCoroutine(GetLocation());
        }
    }

    private IEnumerator GetLocation()
    {
        Input.location.Start();
        while (Input.location.status == LocationServiceStatus.Initializing)
        {
            yield return new WaitForSeconds(0.5f);
        }
        latitude = Input.location.lastData.latitude;
        longitude = Input.location.lastData.longitude;
        yield break;
    }

    // Update is called once per frame
    void Update()
    {

        latitude = Input.location.lastData.latitude;
        longitude = Input.location.lastData.longitude;
        attitude = Input.gyro.attitude.eulerAngles.z;
        gpsText.text = "Lat: " + latitude + "\nLon: " + longitude + "\nAtt: " + attitude;

    }

}
