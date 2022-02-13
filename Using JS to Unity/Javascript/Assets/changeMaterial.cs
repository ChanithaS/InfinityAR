using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeMaterial : MonoBehaviour
{
    public int number;

    public Renderer[] cubeRenderer;
    public GameObject[] cube;
    public Material red;
    public Material green;
    public Material pink;
    public Material blue;
    public Material yellow;
    public Material black;
    
    
    
    
    

    // Start is called before the first frame update
    
    void Start()
    {
        
        cubeRenderer[number] = cube[number].GetComponent<Renderer>();
        
       
        

    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Green")
        {
            Invoke("greenColor", 3);
            

            

            
        }
        if (col.gameObject.tag == "Blue")
        {
            Invoke("blueColor", 3);

        }
        if (col.gameObject.tag == "Yellow")
        {
            Invoke("yellowColor", 3);

        }
        if (col.gameObject.tag == "Red")
        {
            Invoke("redColor", 3);

        }
        if (col.gameObject.tag == "Black")
        {
            Invoke("blackColor", 3); ;

        }
        if (col.gameObject.tag == "Pink")
        {
            Invoke("pinkColor", 3);


        }



    }
    public void greenColor()
    {
        cubeRenderer[number].material = green;

    }
    public void blueColor()
    {
        cubeRenderer[number].material = blue;

    }
    public void yellowColor()
    {
        cubeRenderer[number].material = yellow;

    }
    public void redColor()
    {
        cubeRenderer[number].material = red;

    }
    public void blackColor()
    {
        cubeRenderer[number].material = black;

    }
    public void pinkColor()
    {
        cubeRenderer[number].material = pink;

    }


}
