using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataReceiver : MonoBehaviour
{
    public GameObject[] keyPoints = new GameObject[12];
    public GameObject WholeBody; 
    public Text textright;
    public Text textleft;
    public float leftArm;
    public float rightArm;
    public float nose;

    JsonObject pointsData = new JsonObject();
    Distace distData = new Distace();

    float speed = 0.01f;
    private Vector3 WholebodyCopy;

    public static float distance;

    public void dist(string scaleData){
        distData = JsonUtility.FromJson<Distace>(scaleData);
        distance = distData.scale;
        WholeBody.transform.localScale = new Vector3(distData.scale, distData.scale, distData.scale);
        textleft.text = " right "+distData.scale;
    }

    public void keypointData(string data)
    {
        pointsData = JsonUtility.FromJson<JsonObject>(data);

        float bodyX = (pointsData.data11.x+pointsData.data12.x)/-2;
        float bodyY = (pointsData.data11.y+pointsData.data12.y)/-2;
        float bodyZ = (pointsData.data11.z+pointsData.data12.z)/2;
        //Whole body movement with smoothning
        WholebodyCopy = new Vector3(bodyX, bodyY, bodyZ);
        Vector3 move = new Vector3(bodyX, bodyY, bodyZ);
        WholeBody.transform.position = Vector3.MoveTowards(WholebodyCopy, move, Time.deltaTime * speed);

        float rotate = 0;
        if(pointsData.data12.z < pointsData.data11.z && pointsData.data12.w > 0.7 && pointsData.data12.z - pointsData.data11.z < -1 ){
            //right side
            rotate = pointsData.data12.z * -8;
        }else if(pointsData.data11.z < pointsData.data12.z && pointsData.data11.w > 0.7 && pointsData.data11.z - pointsData.data12.z < -1 ){
            rotate = pointsData.data11.z * 8 ;
        }
        else{
            rotate = 0;
        }
        WholeBody.transform.localEulerAngles = new Vector3(0, rotate, 0);

        //textright.text = " nosez "+pointsData.data11.z;
        //textleft.text = " right "+pointsData.data12.z;
        //Camera canvas renderer
        
        
        //nose
        keyPoints[0].transform.position = new Vector3(-1*pointsData.data0.x, -1*pointsData.data0.y, pointsData.data0.z);
        
        // textright.text = "zaxis - nosez " + pointsData.data11.z +" left " + pointsData.data15.z;
        
        //left shoulder
        keyPoints[1].transform.position = new Vector3(-1*pointsData.data11.x, -1*pointsData.data11.y, pointsData.data11.z);
        //right shoulder
        keyPoints[2].transform.position = new Vector3(-1*pointsData.data12.x, -1*pointsData.data12.y, pointsData.data12.z);
        //left elbow
        keyPoints[3].transform.position = new Vector3(-1*pointsData.data13.x, -1*pointsData.data13.y, pointsData.data13.z);
        //right elbow
        keyPoints[4].transform.position = new Vector3(-1*pointsData.data14.x, -1*pointsData.data14.y, pointsData.data14.z);

        //left wrist
        
        if(pointsData.data15.w > 0.7){   
            keyPoints[5].transform.position = new Vector3(-1*pointsData.data15.x, -1*pointsData.data15.y, pointsData.data15.z);
        }else{
            keyPoints[5].transform.position = new Vector3(-1*pointsData.data0.x-5, -1*pointsData.data0.y-25, pointsData.data0.z);
        }

        //right wrists
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

        nose = pointsData.data0.y;
        leftArm = pointsData.data15.y;
        rightArm = pointsData.data16.y;
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


//-----------------------------------------------------------------------------------------
    public CharacterDatabase characterDB;
    public Text nameText;
    public GameObject[] models;
    public GameObject[] materialObjects;
    public GameObject[] colorButtons;
    public static int currentIndex = 0;


    // Start is called before the first frame update
    void Start()
    {
        UpdateCharacter(currentIndex);
    }

    // Update is called once per frame
    void Update()

    {//if the left arm gose higher than the nose nextOption method callse
        if (rightArm > nose)
        {
            timeRemaining -= Time.deltaTime;
            if (timeRemaining <= 0)
            {
                NextOption();
                timeRemaining = 3f;
            }
        }


        //if the right arm gose higher than the nose backOption method callse
        else if (leftArm > nose)
        {
            timeRemaining -= Time.deltaTime;
            if (timeRemaining <= 0)
            {
                BackOption();
                timeRemaining = 3f;
            }



        }
        else
        {
            timeRemaining = 3f;
        }
        //this method is to cahnge the elemnths (t=shirts) to right side
        public void NextOption()
    {
        UpdateCharacter(currentIndex);
        currentIndex++;
        if (currentIndex == models.Length)
            currentIndex = 0;
        UpdateCharacter(currentIndex);
    }
    // this method is to change the elements (t-shirts) to left side
    public void BackOption()
    {
        UpdateCharacter(currentIndex);
        currentIndex--;
        if (currentIndex == -1)
            currentIndex = models.Length -1 ;
        UpdateCharacter(currentIndex);
    }

    private void UpdateCharacter(int SelectedOption)
    {
        //creating a new character for the selected index
        Character character = characterDB.Getcharacter(SelectedOption);
        //set the name of the selected index
        nameText.text = character.charaterName;

        //setting all gameobjects to set active false and the selected one to active
        foreach (GameObject cloth in models)
            cloth.SetActive(false);

        models[currentIndex].SetActive(true); 

        //setting all gameobjects materials to set active false
        foreach (GameObject mat in materialObjects)
            mat.SetActive(false);

        foreach (GameObject button in colorButtons)
            button.gameObject.SetActive(false);

        //setting the materials for the sepecific gameobjects and set active the no of materials
        for (int i = 0; i < character.materials.Length; i++)
        {
            materialObjects[i].SetActive(true);
            materialObjects[i].GetComponent<Renderer>().material = character.materials[i];

            colorButtons[i].SetActive(true);
            colorButtons[i].GetComponent<Image>().sprite  = character.materialSprites[i];
        }    
    }
}
