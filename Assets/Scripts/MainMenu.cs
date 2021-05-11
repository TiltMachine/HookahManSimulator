using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
   public static event LanguageChangeHandler onLanguageChange;
   public delegate void LanguageChangeHandler();
   public void PlayButton(){
      // Player.SavePlayer();
      SceneManager.LoadScene(1);
   }
   public void changeLanguageTo_Russian(){
      LocalisationSystem.changeLanguageTo_Russian();
      onLanguageChange?.Invoke();
   }
   public void changeLanguageTo_English(){
      LocalisationSystem.changeLanguageTo_English();
      onLanguageChange?.Invoke();

   }
}
