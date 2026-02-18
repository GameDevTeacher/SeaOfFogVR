using System;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class SaveSystem
{
    private static SaveData _saveData = new SaveData();
    
    
    [System.Serializable]
    public struct SaveData
    {
        public PlayerSaveData playerData;
        public BoatSaveData boatData;
        public List<SceneSaveData> sceneData;
        //public EventSaveData eventData; 
    }

    public static string SaveFileName()
    {
        string saveFile = Application.persistentDataPath + "/SaveData" + ".json";
        return saveFile;
    }

    public static void Save()
    {
        HandleSaveData();
        
        File.WriteAllText(SaveFileName(), JsonUtility.ToJson(_saveData, true));
    }

    private static void HandleSaveData()
    {
        SaveManager.instance.player.Save(ref _saveData.playerData);
        SaveManager.instance.boat.Save(ref _saveData.boatData);
        
        #region MultiSceneSaving
        var templist = new List<SceneSaveData>();
        for (int i = 0; i < SaveManager.instance.sceneData.Count; i++)
        {
            SceneSaveData tempData = new SceneSaveData();
            SaveManager.instance.sceneData[i].Save(ref tempData);
            templist.Add(tempData);
        }
        _saveData.sceneData = templist;
        #endregion
        
    }

    public static void Load()
    {
        Debug.Log("loading Triggered");
        string saveContent = File.ReadAllText(SaveFileName());
        _saveData = JsonUtility.FromJson<SaveData>(saveContent);
        HandleLoadData();
    }

    private static async void HandleLoadData()
    {
        try
        {
            //DO NOT FUCK WITH THIS, the order of operations is VITAL
            Debug.Log("starting loading");
            SaveManager.instance.sceneLoader.LoadSceneByIndexSingle(_saveData.sceneData[0].sceneIndex); //load first scene to clear all current loaded scenes
            await Awaitable.NextFrameAsync();
            if (SaveManager.instance.sceneData.Count > 0) //load all additional scenes additively 
            {
                for (int i = 1; i < _saveData.sceneData.Count; i++)
                {
                    SaveManager.instance.sceneLoader.LoadSceneByIndex(_saveData.sceneData[i].sceneIndex);
                }
            }
            SaveManager.instance.AssignVariables();
            SaveManager.instance.player.Load(_saveData.playerData);
            SaveManager.instance.boat.Load(_saveData.boatData);
            Debug.Log("finished loading");
        }
        catch (Exception e)
        {
            Debug.LogError("Error loading: " + e.Message);
            throw; // TODO handle exception
        }
    }
    



}
