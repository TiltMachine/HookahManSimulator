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
    [SerializeField] public List<ShopItem> ShopItemsList_Food;
    [SerializeField] public List<ShopItem> ShopItemsList_Hookah;

    [SerializeField] public List<ShopItem> ShopItemsList_Munchtuk;

    [SerializeField] public List<ShopItem> ShopItemsList_Furniture;

    GameObject currentCategory;

    TextMeshProUGUI balance;

    Player Player;

    GameObject ItemTemplate;
    GameObject g;

    private void Awake() {
        if(SaveSystem.exist_Shop_SaveFile())
            LoadShop();
        else
            SaveShop();
    }
    void Start (){
        

        categoryHolder = GameObject.Find("CategoryHolder");
        currentCategory = GameObject.Find("Content_Food");

        Player = GameObject.Find("Canvas").GetComponent<Player>();

        balance = GameObject.Find("BalanceText").GetComponent<TextMeshProUGUI>();
        balance.SetText((Player.Money).ToString());

        Generate_ALL_Items();
    }
    public void SaveShop(){
        SaveSystem.SaveShop(this);
    }
    public void LoadShop(){
        ShopData data = SaveSystem.LoadShop();

        if(data == null){
            Debug.Log("NO DATA");
        }
        else{
            // for(int i =0; i<ShopItemsList_Food.Count;i++){
            //     ShopItemsList_Food[i].isPurchased = data.ShopItemsList_Food[i];   
            // }
            foreach(ShopItem item in ShopItemsList_Food){
                item.isPurchased = data.ShopItemsList_Food[ShopItemsList_Food.IndexOf(item)];
            }
            foreach(ShopItem item in ShopItemsList_Hookah){
                item.isPurchased = data.ShopItemsList_Hookah[ShopItemsList_Hookah.IndexOf(item)];
            }
            foreach(ShopItem item in ShopItemsList_Munchtuk){
                item.isPurchased = data.ShopItemsList_Munchtuk[ShopItemsList_Munchtuk.IndexOf(item)];
            }
            foreach(ShopItem item in ShopItemsList_Furniture){
                item.isPurchased = data.ShopItemsList_Furniture[ShopItemsList_Furniture.IndexOf(item)];
            }
            // ShopItemsList_Food = data.ShopItemsList_Food;
            // ShopItemsList_Hookah = data.ShopItemsList_Hookah;
            // ShopItemsList_Munchtuk = data.ShopItemsList_Munchtuk;
            // ShopItemsList_Furniture = data.ShopItemsList_Furniture;
        }
    }
    public void Button_CategoryChoose_Clicked(){
        foreach(Transform obj in categoryHolder.transform){
            obj.GetComponent<Image>().color = defaultCategoryColor;
        }
        GameObject parentObj = EventSystem.current.currentSelectedGameObject.transform.parent.gameObject;
        parentObj.GetComponent<Image>().color = clickedCategoryColor;

        ChangeCategory(parentObj.GetComponent<CategoryChooser>().ResponsibleCategory);

    }

    public void ChangeCategory(GameObject newCategory){
        // from.SetActive(false);
        newCategory.transform.parent.transform.parent.GetComponent<ScrollRect>().content = newCategory.GetComponent<RectTransform>();
        currentCategory.SetActive(false);
        currentCategory = newCategory;
        currentCategory.SetActive(true);
    }

    public void GenerateItems(GameObject category, List<ShopItem> list){
        ItemTemplate = category.transform.GetChild(0).gameObject;
        int len = list.Count;
        for (int i=0; i < len; i++) {
            g = Instantiate(ItemTemplate, category.transform);
            g.transform.GetChild(0).GetComponent<Image>().sprite = list[i].Image;
            g.transform.GetChild(1).GetChild(1).GetComponent<TextMeshProUGUI>().SetText(list[i].Price.ToString());
            g.transform.GetChild(2).GetComponent<Button>().interactable = !list[i].isPurchased;
        }
        Destroy(ItemTemplate);
    }
    public void Generate_ALL_Items(){
        GenerateItems(GameObject.Find("Content_Food"),ShopItemsList_Food);
        GenerateItems(GameObject.Find("Content_Hookah"),ShopItemsList_Hookah);
        GameObject.Find("Content_Hookah").SetActive(false);
        GenerateItems(GameObject.Find("Content_Munchtuk"),ShopItemsList_Munchtuk);
        GameObject.Find("Content_Munchtuk").SetActive(false);
        GenerateItems(GameObject.Find("Content_Furniture"),ShopItemsList_Furniture);
        GameObject.Find("Content_Furniture").SetActive(false);
    }


    public void BuyButton_Clicked(){
        GameObject button = EventSystem.current.currentSelectedGameObject;
        int index = button.transform.parent.GetSiblingIndex();
        switch (button.transform.parent.transform.parent.name)
        {
            case "Content_Food":
                if(BuyItem(ShopItemsList_Food[index])){
                    ShopItemsList_Food[index].isPurchased = true;
                    button.GetComponent<Button>().interactable = false;
                    SaveShop();
                }
                break;
                
            case "Content_Hookah":
                if(BuyItem(ShopItemsList_Hookah[index])){
                    ShopItemsList_Hookah[index].isPurchased = true;
                    button.GetComponent<Button>().interactable = false;
                    SaveShop();
                }
                break;  
            case "Content_Munchtuk":
                if(BuyItem(ShopItemsList_Munchtuk[index])){
                    ShopItemsList_Munchtuk[index].isPurchased = true;
                    button.GetComponent<Button>().interactable = false;
                    SaveShop();
                }
                break;
            case "Content_Furniture":
                if(BuyItem(ShopItemsList_Furniture[index])){
                    ShopItemsList_Furniture[index].isPurchased = true;
                    button.GetComponent<Button>().interactable = false;
                    SaveShop();
                }
                break;
        }
    }

    public bool BuyItem(ShopItem item){
        if(Player.Money >= item.Price){
            Player.Money -= item.Price;
            balance.SetText((Player.Money).ToString());
            return true;
        }

        print("Not enough money");
        return false;
        
    }

    }
 