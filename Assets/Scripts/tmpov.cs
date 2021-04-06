using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tmpov : MonoBehaviour
{
    // Start is called before the first frame update
    bool directionR = false;
    private float leftBound;
    private float rightBound;
    private float barWidth;
    
    private float playerSpeed;

    RectTransform canvas;
        
    void Start()
    {
        canvas = GameObject.Find("Canvas").GetComponent<RectTransform>();
        leftBound = -canvas.rect.width/2 + 103;
        rightBound = canvas.rect.width/2 - 103;
        
        barWidth = canvas.rect.width-206;

        // playerSpeed = barWidth/1.5f;
        playerSpeed = Screen.width/1.5f;
        //print("GGGGGGGGGGGGG: " + barWidth/playerSpeed);
        Debug.Log("S: " + barWidth);
        print("V: " + playerSpeed);
        print("T: "+barWidth/playerSpeed);
        //print(GetComponent<RectTransform>().localPosition.x);
    }

    // Update is called once per frame
    void Update()
    {   
        //print(GetComponent<RectTransform>().position.x);
        if(GetComponent<RectTransform>().localPosition.x >= rightBound-10)
            directionR = false;
        if(GetComponent<RectTransform>().localPosition.x <= leftBound+10)
            directionR = true;
        // if(GetComponent<RectTransform>().offsetMin.x >=-50)
        //     directionR = false;
        // if(GetComponent<RectTransform>().offsetMin.x <=-770){
        //     directionR = true;
        // }

        if(directionR)
            transform.position+=-Vector3.left*playerSpeed*Time.deltaTime;
        else
            transform.position+=Vector3.left*playerSpeed*Time.deltaTime;
        
        
            
        
    }
}
