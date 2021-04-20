using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    static string path = Application.persistentDataPath + "/player.glek";

    public static void SavePlayer(Player player){
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(path,FileMode.Create);
        
        PlayerData data = new PlayerData(player);
        formatter.Serialize(stream,data);
        stream.Close();

    }
    public static PlayerData LoadPlayer(){
        if(File.Exists(path)){
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path,FileMode.Open);
            
            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();

            return data;
        }
        else{
            Debug.Log("no such file in "+path);
            return null;
        }

    }
    
    public static bool existSaveFile(){
        return File.Exists(path);
    }
    
}
