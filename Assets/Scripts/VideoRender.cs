using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoRender : MonoBehaviour
{
    //initalizing video screen objects
    public GameObject videoScreen;
    public GameObject ReqScreen;
    public GameObject mainUIObj;
    WebCamTexture _webcamTexture;
    public static bool _enabled = false;

    void Start(){
        //setting the instructions page to true till the button is clicked to get the request
        ReqScreen.SetActive(true);
    }
    public void Enable()
    {
        //after requst is granted
        _enabled = true;
        ReqScreen.SetActive(false);
        mainUIObj.SetActive(true);
    }

    void Update()
    {
        if(_enabled)
        {
            if(_webcamTexture == null)
            {
                while(!Application.RequestUserAuthorization(UserAuthorization.WebCam).isDone)
                {
                    return;
                }
                if (Application.HasUserAuthorization(UserAuthorization.WebCam)) 
                {
                    //Webcam authorized
                    _webcamTexture = new WebCamTexture (WebCamTexture.devices[0].name);
                    _webcamTexture.Play (); 
                } 
                else 
                {
                    // Webcam NOT authorized
                }   
            }
            else if (_webcamTexture.isPlaying)
            {
                if(_webcamTexture.didUpdateThisFrame)
                {                    
                    videoScreen.gameObject.GetComponent<MeshRenderer>().material.SetTexture("_MainTex", _webcamTexture);
                }
            }
        }
    }
}
