using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalisationSystem
{
    public enum Language{
        English,
        Russian

    }

    public static Language language = Language.English;

    private static Dictionary<string,string> LocalisedEN;
    private static Dictionary<string,string> LocalisedRU;
    
    public static bool isInit;

    public static void Init(){
        CSVLoader csvLoader = new CSVLoader();
        csvLoader.LoadCSV();

        LocalisedEN = csvLoader.getDictionaryValues("en");
        LocalisedRU = csvLoader.getDictionaryValues("ru");

        isInit = true;

    }

    public static string GetLocalisedValue(string key){
        if (!isInit)
            Init();

        string value = key;
        switch (language)
        {
            case Language.English:
                LocalisedEN.TryGetValue(key,out value);
                break;
            case Language.Russian:
                LocalisedRU.TryGetValue(key,out value);
                break;
        }

        return value;
    }

    public static void changeLanguageTo_Russian(){
        language = Language.Russian;
    }
    public static void changeLanguageTo_English(){
        language = Language.English;
    }

}
