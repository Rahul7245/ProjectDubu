using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SaveLoadSystem
{
    private const string SAVE_KEY = "CardGameSave";

    public static void SaveGame(CardGameData gameData)
    {
        try
        {
            string json = JsonUtility.ToJson(gameData, true);
            PlayerPrefs.SetString(SAVE_KEY, json);
            PlayerPrefs.Save();
            Debug.Log("Game saved successfully!");
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Failed to save game: {e.Message}");
        }
    }

    public static CardGameData LoadGame()
    {
        try
        {
            if (PlayerPrefs.HasKey(SAVE_KEY))
            {
                string json = PlayerPrefs.GetString(SAVE_KEY);
                CardGameData loadedData = JsonUtility.FromJson<CardGameData>(json);
                Debug.Log("Game loaded successfully!");
                return loadedData;
            }
            else
            {
                Debug.Log("No save data found.");
                return null;
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Failed to load game: {e.Message}");
            return null;
        }
    }

    public static bool HasSaveData()
    {
        return PlayerPrefs.HasKey(SAVE_KEY);
    }

    public static void DeleteSave()
    {
        PlayerPrefs.DeleteKey(SAVE_KEY);
        PlayerPrefs.Save();
        Debug.Log("Save data deleted.");
    }
}