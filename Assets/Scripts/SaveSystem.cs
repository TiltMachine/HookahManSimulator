using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    static string path_Player_SaveFile = Application.persistentDataPath + "/player.glek";
    static string path_Shop_SaveFile = Application.persistentDataPath + "/shop.glek";


    public static void SavePlayer(Player player){
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(path_Player_SaveFile,FileMode.Create);
        
        PlayerData data = new PlayerData(player);
        formatter.Serialize(stream,data);
        stream.Close();

    }
    public static PlayerData LoadPlayer(){
        if(File.Exists(path_Player_SaveFile)){
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path_Player_SaveFile,FileMode.Open);
            
            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();

            return data;
        }
        else{
            Debug.Log("no such file in "+path_Player_SaveFile);
            return null;
        }

    }

    public static void SaveShop(ShopScript shop){
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(path_Shop_SaveFile,FileMode.Create);

        ShopData data = new ShopData(shop);
        formatter.Serialize(stream,data);
        stream.Close();

    }
    public static ShopData LoadShop(){
        if(File.Exists(path_Shop_SaveFile)){
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path_Shop_SaveFile,FileMode.Open);
            
            ShopData data = formatter.Deserialize(stream) as ShopData;
            stream.Close();

            return data;
        }
        else{
            Debug.Log("no such file in "+path_Shop_SaveFile);
            return null;
        }

    }
    
    public static bool exist_Player_SaveFile(){
        return File.Exists(path_Player_SaveFile);
    }
    public static bool exist_Shop_SaveFile(){
        return File.Exists(path_Shop_SaveFile);
    }
    
}
