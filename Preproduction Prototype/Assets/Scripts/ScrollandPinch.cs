using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ScrollandPinch : MonoBehaviour
{


    [SerializeField]
    private CinemachineFreeLook cvc;
    [SerializeField]
    private Camera cam;
    [SerializeField]
    private GameObject player;

    private bool rotType;

    // Start is called before the first frame update
    void Start()
    {
        rotType = false;    // Set initial rotation type to be gyroscopic

        Input.gyro.enabled = true;      // Enable the use of the phones gyroscope

    }

    // Update is called once per frame
    void Update()
    {
        // If the rotation type is gyroscopic
        if (!rotType)
        {
            cvc.m_XAxis.Value = (Input.gyro.attitude.eulerAngles.z - 180) * -1;     // Set the value of the cinemachine camera X axis to the gyroscopic rotaion of the phones z axis converted to a range of 0 -> 360

        }

        // Count number of touch inputs
        switch (Input.touchCount)
        {
            // If there is 1 touch registered
            case 1:
                
                // If the rotation type is manual
                if (rotType)
                {                    
                    Touch touch = Input.GetTouch(0);    // Get instance of touch

                    
                    Vector2 touchPrevPos = touch.position - touch.deltaPosition;    // Find the position of the touch in the last update                    
                    Vector2 playerPos = cam.WorldToScreenPoint(player.transform.position);      // Find the current position of the touch

                    float prevAngle = Mathf.Atan2(touchPrevPos.y - playerPos.y, touchPrevPos.x - playerPos.x);      // Find the angle of the last touch position ralative to the players on screen coords
                    prevAngle = prevAngle * Mathf.Rad2Deg;      // Convert the previous angle to degrees from radians
                    if (prevAngle < 0) { prevAngle += 360; }    // convert the previous angle from a range of -180 -> 180 to 0 -> 360

                    float Angle = Mathf.Atan2(touch.position.y - playerPos.y, touch.position.x - playerPos.x);      // Find the angle of the current touch position ralative to the players on screen coords
                    Angle = Angle * Mathf.Rad2Deg;      // Convert the current angle to degrees from radians
                    if (Angle < 0) { Angle += 360; }    // convert the current angle from a range of -180 -> 180 to 0 -> 360


                    // Ignore changes in the angle of over 300 degrees to account for the point where 360 goes to 0
                    if (((Angle - prevAngle) < 300) && ((Angle - prevAngle) > -300)) { cvc.m_XAxis.Value += (Angle - prevAngle) / 2; }      // Add half of the change in angle to the cinemachine camera X axis
                                      
                }

                break;  // End the current switch case

            // If there is 2 touches registered
            case 2:

                Touch touchZero = Input.GetTouch(0);    // Get instance of first touch
                Touch touchOne = Input.GetTouch(1);     // Get instance of second touch

                Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;    // Find the position of the first touch in the last update
                Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;       // Find the position of the second touch in the last update

                float prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;       // Find the distance between the 2 touches in the previous update
                float currentMagnitude = (touchZero.position - touchOne.position).magnitude;    // Find the current distance between the 2 touches

                float difference = currentMagnitude - prevMagnitude;    // Calculate the change in distance between the 2 touchs from the last update to now


                cvc.m_YAxis.Value -= (difference * 0.001f);     // Subtract the change in distance multiplied by a constant from the cinemachine cameras Y axis

                break;  // End the current switch case
        }
    }

    public void MoveZoom()
    {
        // If the camera is currently fully zoomed out
        if (cvc.m_YAxis.Value == 1)
        {
            cvc.m_YAxis.Value = 0;      // Set the camera to fully zoomed in
        }
        // Otherwise if the camera is not fully zoomed out
        else
        {
            cvc.m_YAxis.Value = 1;      // Set the camera to fully zoomed out
        }
    }

    public void SwitchRotType()
    {
        //rotType = !rotType;     // Toggle between the rotation types

        if (rotType) { rotType = false; }
        else if (!rotType) { rotType = true; }
    }



}
