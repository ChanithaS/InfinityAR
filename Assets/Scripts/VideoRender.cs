using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoRender : MonoBehaviour
{
    public GameObject videoScreen;
    public GameObject ReqScreen;
    public GameObject mainUIObj;
    WebCamTexture _webcamTexture;
    bool _enabled;

    void Start(){
        ReqScreen.SetActive(true);
    }
    public void Enable()
    {
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
