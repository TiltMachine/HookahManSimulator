using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static void LoadGameScene(){
        SceneManager.LoadScene(1);
    }
    public static void LoadShopScene(){
        SceneManager.LoadScene(2);
    }
}
