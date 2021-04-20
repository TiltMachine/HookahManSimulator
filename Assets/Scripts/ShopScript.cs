using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShopScript : MonoBehaviour
{
    public void ButtonClose()
    {
        SceneManager.LoadScene(1);
    }
    [System.Serializable] class ShopItem
    {
        public Sprite Image;
        public int Price;
        public bool isPurchased = false;
    }

    [SerializeField] List<ShopItem> ShopItemsList;

    GameObject ItemTemplate;
    GameObject g;
   [SerializeField] Transform ShopScrollView;
    void Start ()
    {
        ItemTemplate = ShopScrollView.GetChild (0).gameObject;

        int len = ShopItemsList.Count;
        for (int i=0; i < len; i++) {
            g = Instantiate(ItemTemplate, ShopScrollView);
            g.transform.GetChild (0).GetComponent <Image> ().sprite = ShopItemsList [i].Image;
            g.transform.GetChild (1).GetChild (0).GetComponent <Text> ().text = ShopItemsList [i].Price.ToString ();
            g.transform.GetChild (2).GetComponent <Button> ().interactable = !ShopItemsList [i].isPurchased;

        }
        Destroy(ItemTemplate);
    }

    }
 