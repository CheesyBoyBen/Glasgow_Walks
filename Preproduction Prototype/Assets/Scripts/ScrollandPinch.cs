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
        rotType = false;

        Input.gyro.enabled = true;

    }

    // Update is called once per frame
    void Update()
    {
        

        switch (Input.touchCount)
        {
            case 1:
                
                if (rotType)
                {
                    Touch touch = Input.GetTouch(0);

                    /*if (touch.position.x > (touch.position - touch.deltaPosition).x)
                    {
                        cvc.m_XAxis.Value -= (touch.deltaPosition.magnitude * 0.05f);
                    }
                    else
                    {
                        cvc.m_XAxis.Value += (touch.deltaPosition.magnitude * 0.05f);
                    }*/

                    Vector2 touchPrevPos = touch.position - touch.deltaPosition;
                    Vector2 playerPos = cam.WorldToScreenPoint(player.transform.position);

                    float prevAngle = Mathf.Atan2(touchPrevPos.y - playerPos.y, touchPrevPos.x - playerPos.x);
                    prevAngle = prevAngle * Mathf.Rad2Deg;
                    if (prevAngle < 0) { prevAngle += 360; }

                    float Angle = Mathf.Atan2(touch.position.y - playerPos.y, touch.position.x - playerPos.x);
                    Angle = Angle * Mathf.Rad2Deg;
                    if (Angle < 0) { Angle += 360; }


                    if (((Angle - prevAngle) < 300) && ((Angle - prevAngle) > -300)) { cvc.m_XAxis.Value += (Angle - prevAngle) / 2; }
                                        
                }

                if (!rotType)
                {
                    Debug.Log(Input.gyro.attitude.eulerAngles.x);
                    cvc.m_XAxis.Value = (Input.gyro.attitude.eulerAngles.z - 180) * -1;

                }

                break;

            case 2:
                Touch touchZero = Input.GetTouch(0);
                Touch touchOne = Input.GetTouch(1);

                Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
                Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

                float prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
                float currentMagnitude = (touchZero.position - touchOne.position).magnitude;

                float difference = currentMagnitude - prevMagnitude;


                cvc.m_YAxis.Value -= (difference * 0.001f);

                break;
        }
    }

    public void MoveZoom()
    {
        if (cvc.m_YAxis.Value == 1)
        {
            cvc.m_YAxis.Value = 0;
        }
        else
        {
            cvc.m_YAxis.Value = 1;

        }
    }

    public void SwitchRotType()
    {        
        if (rotType) { rotType = false; }
        else if (!rotType) { rotType = true; }        
    }



}
