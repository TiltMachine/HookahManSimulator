using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class PlayerStats : MonoBehaviour
{
    private int maxHealth = 4;
    private float maxHeartBeat = 3.5f;
    private float currentHeartBeat = 1;
    private float heartBeatIncreaseSpeed = 0.3f;
    private float heartBeatDecreaseSpeed = 0.2f;
    // хз зачем это поле пока что (отвечает за скорость смоке слайдера по факту)
    private float maxSmokeCapacity = 0.2f;
    private float temperature_speed_up = 0.01f;
    private float temperature_speed_down = 0.007f;
    public int currentHealth {get; set;}
    public float MaxSmokeCapacity { get => maxSmokeCapacity; set => maxSmokeCapacity = value; }
    public float CurrentHeartBeat { get => currentHeartBeat; set => currentHeartBeat = value; }
    public float MaxHeartBeat { get => maxHeartBeat; set => maxHeartBeat = value; }
    public int MaxHealth { get => maxHealth; set => maxHealth = value; }
    public float Temperature_speed_up { get => temperature_speed_up; set => temperature_speed_up = value; }
    public float Temperature_speed_down { get => temperature_speed_down; set => temperature_speed_down = value; }

    private string pathToHearts;
    Smoke Smoke;

    private void Awake() {
        
        currentHealth = MaxHealth;
        pathToHearts = "heart_HP/"+currentHealth+"HP/";
        Smoke = GetComponent<Smoke>();
    }
    
    public void IncreaseHeartBeat(){
        CurrentHeartBeat += heartBeatIncreaseSpeed*Time.deltaTime;
        if(CurrentHeartBeat>=MaxHeartBeat)
            TakeDamage();
    }
    public void DecreaseHeartBeat(){
        if(CurrentHeartBeat>1)
            CurrentHeartBeat -= heartBeatDecreaseSpeed*Time.deltaTime;
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


