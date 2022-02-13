using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataReceiver : MonoBehaviour
{
    public static GameObject[] keyPoints = new GameObject[12];
    public GameObject WholeBody; 
    public Text textright;
    public Text textleft;

    JsonObject pointsData = new JsonObject();
    Distace distData = new Distace();

    float speed = 0.01f;
    private Vector3 WholebodyCopy;

    public void dist(string scaleData){
        distData = JsonUtility.FromJson<Distace>(scaleData);
        WholeBody.transform.localScale = new Vector3(distData.scale, distData.scale, 1);
    }

    public void keypointData(string data)
    {
        pointsData = JsonUtility.FromJson<JsonObject>(data);

        //Whole body movement with smoothning
        WholebodyCopy = new Vector3(-1*pointsData.data0.x, -1*pointsData.data0.y, pointsData.data0.z);
        Vector3 move = new Vector3(-1*pointsData.data0.x, -1*pointsData.data0.y, pointsData.data0.z);
        WholeBody.transform.position = Vector3.MoveTowards(WholebodyCopy, move, Time.deltaTime * speed);

        //Camera canvas renderer
        
        
        //nose
        keyPoints[0].transform.position = new Vector3(-1*pointsData.data0.x, -1*pointsData.data0.y, pointsData.data0.z);
        
        // textright.text = "zaxis - nosez " + pointsData.data11.z +" left " + pointsData.data15.z;
        textleft.text = " right "+pointsData.data16.z;
        //left shoulder
        keyPoints[1].transform.position = new Vector3(-1*pointsData.data11.x, -1*pointsData.data11.y, pointsData.data11.z);
        //right shoulder
        keyPoints[2].transform.position = new Vector3(-1*pointsData.data12.x, -1*pointsData.data12.y, pointsData.data12.z);
        //left elbow
        keyPoints[3].transform.position = new Vector3(-1*pointsData.data13.x, -1*pointsData.data13.y, pointsData.data13.z);
        //right elbow
        keyPoints[4].transform.position = new Vector3(-1*pointsData.data14.x, -1*pointsData.data14.y, pointsData.data14.z);

        //left wrist
        textleft.text = " left "+pointsData.data15.w;
        if(pointsData.data15.w > 0.7){   
            keyPoints[5].transform.position = new Vector3(-1*pointsData.data15.x, -1*pointsData.data15.y, pointsData.data15.z);
        }else{
            keyPoints[5].transform.position = new Vector3(-1*pointsData.data0.x-5, -1*pointsData.data0.y-25, pointsData.data0.z);
        }

        //right wrist
        textright.text = " right "+pointsData.data16.w;       
        if(pointsData.data16.w > 0.7){    
            keyPoints[6].transform.position = new Vector3(-1*pointsData.data16.x, -1*pointsData.data16.y, pointsData.data16.z);  
        }else{
            keyPoints[6].transform.position = new Vector3(-1*pointsData.data0.x+5, -1*pointsData.data0.y-25, pointsData.data0.z);
        }
        
        
        //left hip
        keyPoints[7].transform.position = new Vector3(-1*pointsData.data23.x, -1*pointsData.data23.y, pointsData.data23.z);
        //right hip
        keyPoints[8].transform.position = new Vector3(-1*pointsData.data24.x, -1*pointsData.data24.y, pointsData.data24.z);
        //left knee
        keyPoints[9].transform.position = new Vector3(-1*pointsData.data25.x, -1*pointsData.data25.y, pointsData.data25.z);
        //right knee
        keyPoints[10].transform.position = new Vector3(-1*pointsData.data26.x, -1*pointsData.data26.y, pointsData.data26.z);
        //left ankle
        keyPoints[11].transform.position = new Vector3(-1*pointsData.data27.x, -1*pointsData.data27.y, pointsData.data27.z);
        //right ankle
        keyPoints[12].transform.position = new Vector3(-1*pointsData.data28.x, -1*pointsData.data28.y, pointsData.data28.z);
    }

    public class JsonObject{
        public Vector4 data0;

        public Vector4 data1;
        public Vector4 data2;
        
        public Vector4 data11;
        public Vector4 data12;
        public Vector4 data13;
        public Vector4 data14;
        public Vector4 data15;
        public Vector4 data16;

        public Vector4 data23;
        public Vector4 data24;
        public Vector4 data25;
        public Vector4 data26;
        public Vector4 data27;
        public Vector4 data28;
    }

    public class Distace{
        public float scale;
    }
}