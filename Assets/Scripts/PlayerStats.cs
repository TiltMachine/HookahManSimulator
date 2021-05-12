using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class PlayerStats : MonoBehaviour
{
    public int maxHealth = 4;
    public float heartBeat = 1;
    public float heartBeatIncreaseSpeed = 1f;
    public float heartBeatDecreaseSpeed = 0.5f;

    public int currentHealth {get; set;}

    private void Awake() {
        currentHealth = maxHealth;
    }
    
    public void IncreaseHeartBeat(){
        heartBeat += heartBeatIncreaseSpeed*Time.deltaTime;
    }
    public void DecreaseHeartBeat(){
        heartBeat -= heartBeatDecreaseSpeed*Time.deltaTime;
    }
    
    public void TakeDamage(){
        currentHealth-=1;
        Debug.Log("Player took 1 damage");

        if(currentHealth <= 0){
            Die();
        }
    }

    public void Die(){
        Debug.Log("Player is dead");
<<<<<<< Updated upstream
=======
        Smoke.Lose();

>>>>>>> Stashed changes
    }
}


