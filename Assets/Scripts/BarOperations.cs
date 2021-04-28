using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarOperations : MonoBehaviour
{
    // Start is called before the first frame update
    bool directionR = false;
    private float leftBound;
    private float rightBound;
    private float barWidth;
    
    private float playerSpeed;


    RectTransform canvas;

    private GameObject parentSlider;
    private GameObject playerBar;
        
    ArrayList spawnedObjects = new ArrayList();
    ArrayList objects_speed = new ArrayList();

    private float RATIO_GREEN = 25;
    private float RATIO_RED = 25;
    private float RATIO_WHITE = 50;

    public bool inCollisionG = false;
    public bool inCollisionR = false;
    public bool inCollisionW = false;

    public GameObject colidedObject;

    // private float boxScale_COEF;

    public int r = 0;
    public int g = 0;
    public int w = 0;
    public int total = 0;

    void Start()
    {
        canvas = GameObject.Find("Canvas").GetComponent<RectTransform>();
        leftBound = -canvas.rect.width/2 + 103;
        rightBound = canvas.rect.width/2 - 103;
        
        barWidth = canvas.rect.width-206;

        // playerSpeed = barWidth/1.5f;
        playerSpeed = Screen.width/1.5f;

        // boxScale_COEF  = barWidth/772;
        

        parentSlider = GameObject.Find("Bar");
        playerBar = GameObject.Find("Bar/PlayerBar");
        //print("GGGGGGGGGGGGG: " + barWidth/playerSpeed);
        // Debug.Log("bar width: " + barWidth);
        
        // print("V: " + playerSpeed);
        // print("T: "+barWidth/playerSpeed);

        // StartSpawning();
        // RATIO_WHITE = 50 + FindX(RATIO_WHITE,RATIO_GREEN,RATIO_RED,0.2f);
        // RATIO_GREEN = RATIO_GREEN + FindX(RATIO_GREEN,RATIO_WHITE,RATIO_RED,0.9f);

        //   for(int i=0;i<100000;i++){
        //     total++;
        //     Rand();    
        // }
        // Debug.Log("G: "+ g*100.0/total +"\nR: "+r*100.0/total + "\nW: "+w*100.0/total);
    }

    // Update is called once per frame
    void Update()
    {   

        if(GetComponent<RectTransform>().localPosition.x >= rightBound-10)
            directionR = false;
        if(GetComponent<RectTransform>().localPosition.x <= leftBound+10)
            directionR = true;

        if(directionR)
            transform.position+=-Vector3.left*playerSpeed*Time.deltaTime;
        else
            transform.position+=Vector3.left*playerSpeed*Time.deltaTime;
        
        
        MoveObjects();
        
        
        
        
        
    }
    void Rand(){
        float x = Random.Range(0,RATIO_GREEN+RATIO_RED+RATIO_WHITE);

        if ((x -= RATIO_GREEN) < 0){ 
            g++;
        } 
        else if ((x -= RATIO_RED) < 0){ 
            r++;
        }
        else{ 
            w++;
        }
        // Debug.Log("G: "+ g*100.0/total +"\nR: "+r*100.0/total + "\nW: "+w*100.0/total);

    }
    public float FindX(float change, float a, float b, float p){
        return (((a+b)*(p*(a+b+change)+change))/((a+b+change)*(1-p)-change)) - change;
    }

    void Spawn(){
        GameObject NewObj = new GameObject(); //Create the GameObject
        Image NewImage = NewObj.AddComponent<Image>(); //Add the Image Component script
        NewObj.AddComponent<BoxCollider2D>();
        NewObj.GetComponent<BoxCollider2D>().size = new Vector2(100f, 100f);
        NewObj.GetComponent<RectTransform>().SetParent(parentSlider.transform);
        NewObj.GetComponent<RectTransform>().localPosition = new Vector3(rightBound-10,transform.localPosition.y,transform.localPosition.z);
        NewObj.GetComponent<RectTransform>().localScale = new Vector3(1,0.8f,0);
        NewObj.transform.SetSiblingIndex(2);

        SelectColor(NewObj);
        ApplyRandomScale(NewObj);

        NewObj.SetActive(true);
        
        spawnedObjects.Add(NewObj);
        objects_speed.Add(Random.Range(playerSpeed/4,playerSpeed/1.5f));
    }

    void ApplyRandomScale(GameObject obj){
        //TODO: scale width относительно ширины экрана
        float rand = Random.value + 0.4f;
        obj.transform.localScale = new Vector3(obj.transform.localScale.x*rand,obj.transform.localScale.y,0);
    }
    void MoveObjects(){
        
        for (int i = spawnedObjects.Count - 1; i >= 0; i--){
            GameObject obj = (GameObject)spawnedObjects[i];
            if(obj.GetComponent<RectTransform>().localPosition.x <= leftBound+10){
                // print(spawnedObjects.IndexOf(obj) + " " + i);
                Destroy(obj);
                spawnedObjects.Remove(obj);
                
                objects_speed.RemoveAt(i);
            }
            else
                obj.transform.position+=Vector3.left*(float)objects_speed[i]*Time.deltaTime;
        }
    }
    void SelectColor(GameObject obj){
        float x = Random.Range(0,RATIO_GREEN+RATIO_RED+RATIO_WHITE);

        if ((x -= RATIO_GREEN) < 0){ 
            obj.tag = "GreenBlock";
            obj.GetComponent<Image>().color = Color.green;
        } 
        else if ((x -= RATIO_RED) < 0){ 
            obj.tag = "RedBlock";
            obj.GetComponent<Image>().color = Color.red;
        }
        else{ 
            obj.tag = "WhiteBlock";
            obj.GetComponent<Image>().color = Color.gray;
        }

    }
    
    private void OnTriggerEnter2D(Collider2D other) {
        colidedObject = other.gameObject;
        if(other.gameObject.tag == "GreenBlock"){
            inCollisionG = true;
            // print("G");
            // DestroyObject(other.gameObject);
        }
        else if(other.gameObject.tag == "RedBlock"){
            inCollisionR = true;
            // print("R");
        }
        else if(other.gameObject.tag == "WhiteBlock"){
            inCollisionW = true;
            // print("W");
            // DestroyOFbject(other.gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.tag == "GreenBlock"){
            inCollisionG = false;
        }
        else if(other.gameObject.tag == "RedBlock"){
            inCollisionR = false;
        }
        else if(other.gameObject.tag == "WhiteBlock"){
            inCollisionW = false;
        }
    }

    public void DestroyObject(GameObject obj){
        Destroy(obj);
        objects_speed.RemoveAt(spawnedObjects.IndexOf(obj));
        spawnedObjects.Remove(obj);        
    }
    public void StopSpawning(){
        CancelInvoke("Spawn");
        // print(spawnedObjects.Count);
        
        for (int i = spawnedObjects.Count - 1; i >= 0; i--){
            DestroyObject((GameObject)(spawnedObjects[i]));
        }
        
    }
    public void StartSpawning(){
        InvokeRepeating("Spawn", 2, 1f);
        
    }
}
