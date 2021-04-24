using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ShopData
{
    public List<bool> ShopItemsList_Food = new List<bool>();
    public List<bool> ShopItemsList_Hookah = new List<bool>();

    public List<bool> ShopItemsList_Munchtuk = new List<bool>();

    public List<bool> ShopItemsList_Furniture = new List<bool>();

    public ShopData(ShopScript shop){
        ShopItemsList_Food.Clear();
        ShopItemsList_Hookah.Clear();
        ShopItemsList_Munchtuk.Clear();
        ShopItemsList_Furniture.Clear();

        foreach(ShopItem item in shop.ShopItemsList_Food){
            ShopItemsList_Food.Add(item.isPurchased);
        }
        foreach(ShopItem item in shop.ShopItemsList_Hookah){
            ShopItemsList_Hookah.Add(item.isPurchased);
        }
        foreach(ShopItem item in shop.ShopItemsList_Munchtuk){
            ShopItemsList_Munchtuk.Add(item.isPurchased);
        }
        foreach(ShopItem item in shop.ShopItemsList_Furniture){
            ShopItemsList_Furniture.Add(item.isPurchased);
        }

        // ShopItemsList_Food = shop.ShopItemsList_Food.;
        // ShopItemsList_Hookah = shop.ShopItemsList_Hookah;
        // ShopItemsList_Munchtuk = shop.ShopItemsList_Munchtuk;
        // ShopItemsList_Furniture = shop.ShopItemsList_Furniture;
    }
    
}
