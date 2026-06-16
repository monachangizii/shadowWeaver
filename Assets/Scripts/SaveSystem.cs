using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
using UnityEngine;

public static class SaveSystem
{
    // The key used to store data in PlayerPrefs
    private const string SAVE_KEY = "ShadowWeaver_SaveData";

    public static void SaveGame(float pX, float pY, bool mirrorWorld, string currentScene)
    {
        SaveData data = new SaveData();
        data.playerX = pX;
        data.playerY = pY;
        data.isMirrorWorld = mirrorWorld;
        data.sceneName = currentScene;

        string json = JsonUtility.ToJson(data);

        PlayerPrefs.SetString(SAVE_KEY, json);
        PlayerPrefs.Save(); 

        Debug.Log("[SaveSystem] Game saved successfully! Position: (" + pX + ", " + pY + "), Mirror World: " + mirrorWorld);
    }

    public static SaveData LoadGame()
    {
        if (PlayerPrefs.HasKey(SAVE_KEY))
        {
            string json = PlayerPrefs.GetString(SAVE_KEY);
            
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            Debug.Log("[SaveSystem] Game loaded successfully!");
            return data;
        }
        else
        {
            Debug.Log("[SaveSystem] No save data found. Using default values.");
            return new SaveData(); 
        }
    }

    public static void DeleteSave()
    {
        PlayerPrefs.DeleteKey(SAVE_KEY);
        PlayerPrefs.Save();
        Debug.Log("[SaveSystem] Save data deleted successfully.");
    }
}