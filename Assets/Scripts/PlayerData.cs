using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int maxHealth;
    public float maxHeartBeat;
    public float heartBeatIncreaseSpeed;
    public float heartBeatDecreaseSpeed;
    // хз зачем это поле пока что (отвечает за скорость смоке слайдера по факту)
    public float maxSmokeCapacity;
    public float temperature_speed_up;
    public float temperature_speed_down;
    public int money;
    public float RATIO_GREEN;
    public float RATIO_RED;
    public float RATIO_WHITE;

    public PlayerData(Player player){
        maxHealth = player.MaxHealth;
        maxHeartBeat = player.MaxHeartBeat;
        heartBeatIncreaseSpeed = player.HeartBeatIncreaseSpeed;
        heartBeatDecreaseSpeed = player.HeartBeatDecreaseSpeed;
        maxSmokeCapacity = player.MaxSmokeCapacity;
        temperature_speed_up = player.Temperature_speed_up;
        temperature_speed_down = player.Temperature_speed_down;
        money = player.Money;
        RATIO_GREEN = player.RATIO_GREEN1;
        RATIO_RED = player.RATIO_RED1;
        RATIO_WHITE = player.RATIO_WHITE1;
    }
}
