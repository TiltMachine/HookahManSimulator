using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class ShopScript : MonoBehaviour
{
    Color clickedCategoryColor = new Color(60/255f,33/255f,3/255f,197/255f);
    Color defaultCategoryColor = new Color(155/255f,80/255f,0/255f,197/255f);
    GameObject categoryHolder;
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
    [SerializeField] GameObject ChosenCategory;

    GameObject ItemTemplate;
    GameObject g;
   [SerializeField] Transform ShopScrollView;
    void Start ()
    {
        categoryHolder = GameObject.Find("CategoryHolder");
        
        ItemTemplate = ShopScrollView.GetChild(0).gameObject;

        int len = ShopItemsList.Count;
        for (int i=0; i < len; i++) {
            g = Instantiate(ItemTemplate, ShopScrollView);
            g.transform.GetChild(0).GetComponent<Image>().sprite = ShopItemsList[i].Image;
            g.transform.GetChild(1).GetChild(1).GetComponent<TextMeshProUGUI>().SetText(ShopItemsList[i].Price.ToString());
            g.transform.GetChild(2).GetComponent<Button>().interactable = !ShopItemsList[i].isPurchased;
        }
        Destroy(ItemTemplate);
    }


    public void Clicked(){
        foreach(Transform obj in categoryHolder.transform){
            obj.GetComponent<Image>().color = defaultCategoryColor;
        }
        EventSystem.current.currentSelectedGameObject.transform.parent.gameObject.GetComponent<Image>().color = clickedCategoryColor;
    }

    }
 