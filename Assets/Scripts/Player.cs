using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Player : MonoBehaviour
{
    private int maxHealth = 3;
    private int money = 0;
    private float maxHeartBeat = 3.5f;
    private float heartBeatIncreaseSpeed = 0.3f;
    private float heartBeatDecreaseSpeed = 0.2f;
    // хз зачем это поле пока что (отвечает за скорость смоке слайдера по факту)
    private float maxSmokeCapacity = 0.2f;
    private float temperature_speed_up = 0.012f;
    private float temperature_speed_down = 0.007f;

    private float currentHeartBeat = 1;

    private float RATIO_GREEN = 25;
    private float RATIO_RED = 25;
    private float RATIO_WHITE = 50;
    public int currentHealth {get; set;}
    
    public float MaxSmokeCapacity { get => maxSmokeCapacity; set => maxSmokeCapacity = value; }
    public float CurrentHeartBeat { get => currentHeartBeat; set => currentHeartBeat = value; }
    public float MaxHeartBeat { get => maxHeartBeat; set => maxHeartBeat = value; }
    public int MaxHealth { get => maxHealth; set { maxHealth = value; SavePlayer();} }
    public float Temperature_speed_up { get => temperature_speed_up; set => temperature_speed_up = value; }
    public float Temperature_speed_down { get => temperature_speed_down; set => temperature_speed_down = value; }
    public float HeartBeatIncreaseSpeed { get => heartBeatIncreaseSpeed; set { heartBeatIncreaseSpeed = value; SavePlayer(); }}
    public float HeartBeatDecreaseSpeed { get => heartBeatDecreaseSpeed; set { heartBeatDecreaseSpeed = value;SavePlayer(); } }
    public int Money
    { get => money; 
    set {
            money = value;
            SavePlayer();
        }
    }

    public float RATIO_GREEN1 { get => RATIO_GREEN; set { RATIO_GREEN = value; SavePlayer();} }
    public float RATIO_RED1 { get => RATIO_RED; set { RATIO_RED = value; SavePlayer();} }
    public float RATIO_WHITE1 { get => RATIO_WHITE; set { RATIO_WHITE = value; SavePlayer();} }

    private string pathToHearts;
    Smoke Smoke;

    private void Awake() {
        if(SaveSystem.exist_Player_SaveFile())
            LoadPlayer();
        else
            SavePlayer();
            
        currentHealth = MaxHealth;
        pathToHearts = "heart_HP/"+currentHealth+"HP/";
        Smoke = GetComponent<Smoke>();
        
    }

    public void SavePlayer(){
        SaveSystem.SavePlayer(this);
    }
    public void LoadPlayer(){
        PlayerData data = SaveSystem.LoadPlayer();

        if(data == null){
            Debug.Log("NO DATA");
        }
        else{
            maxHealth = data.maxHealth;
            maxHeartBeat = data.maxHeartBeat;
            heartBeatIncreaseSpeed = data.heartBeatIncreaseSpeed;
            heartBeatDecreaseSpeed = data.heartBeatDecreaseSpeed;
            maxSmokeCapacity = data.maxSmokeCapacity;
            temperature_speed_up = data.temperature_speed_up;
            temperature_speed_down = data.temperature_speed_down;
            money = data.money;
            RATIO_GREEN = data.RATIO_GREEN;
            RATIO_RED = data.RATIO_RED;
            RATIO_WHITE = data.RATIO_WHITE;
        }
    }
    
    public void IncreaseHeartBeat(){
        CurrentHeartBeat += HeartBeatIncreaseSpeed*Time.deltaTime;
        if(CurrentHeartBeat>=MaxHeartBeat)
            TakeDamage();
    }
    public void DecreaseHeartBeat(){
        if(CurrentHeartBeat>1)
            CurrentHeartBeat -= HeartBeatDecreaseSpeed*Time.deltaTime;
        else
            CurrentHeartBeat = 1;
    }
    
    public void TakeDamage(){
        currentHealth-=1;
        GameObject.Find("Heart").GetComponent<Image>().sprite = Resources.Load<Sprite>(pathToHearts + (MaxHealth - currentHealth));

        Debug.Log("Player took 1 damage");

        if(currentHealth <= 0){
            Die();
        }

        Smoke.AfterSmoking();
        Smoke.CoughSound.Play();
    }

    public void Die(){
        Debug.Log("Player is dead");
        Smoke.Lose();
        
    }

    public void ResetAllValues(){
        currentHealth = maxHealth;
        currentHeartBeat = 1;
        GameObject.Find("Heart").GetComponent<Image>().sprite = Resources.Load<Sprite>(pathToHearts+ 0);
    }
    
}


