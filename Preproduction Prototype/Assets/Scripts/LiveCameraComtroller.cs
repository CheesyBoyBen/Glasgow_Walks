using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class LiveCameraComtroller : MonoBehaviour
{
        public WebCamTexture mCamera = null;
        public GameObject plane;
        public GameObject cvc;
        string frontCamName = null;

    // Use this for initialization
    void Start ()
    {
        plane = GameObject.FindWithTag ("LiveCam");                

        var webCamDevices = WebCamTexture.devices;
        foreach (var camDevice in webCamDevices)
        {
            if (camDevice.isFrontFacing)
            {
                frontCamName = camDevice.name;
                break;
                
            }

        }
        mCamera = new WebCamTexture(frontCamName);
        plane.GetComponent<Renderer>().material.mainTexture = mCamera;
        mCamera.Play ();

        plane.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetActive()
    {

        if (plane.activeSelf)
        {
            plane.SetActive(false);
            SavePic();
            cvc.gameObject.GetComponent<ScrollandPinch>().MoveZoom();

        }
        else
        {
            plane.SetActive(true);
            cvc.gameObject.GetComponent<ScrollandPinch>().MoveZoom();
        }
    }

    void SavePic()
    {
        //AssetDatabase.CreateAsset(mCamera, "Assets/SavedPics/Pic.png");
    }
}
