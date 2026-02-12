using System.Collections.Generic;
using UnityEngine;
using System.IO;

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
        string saveContent = File.ReadAllText(SaveFileName());
        _saveData = JsonUtility.FromJson<SaveData>(saveContent);
        HandleLoadData();
    }

    private static void HandleLoadData()
    {
        for (int i = 0; i < _saveData.sceneData.Count; i++)
        {
            SaveManager.instance.sceneLoader.LoadSceneByIndex(_saveData.sceneData[i].sceneIndex);
        }
        SaveManager.instance.player.Load(_saveData.playerData);
        SaveManager.instance.boat.Load(_saveData.boatData);
        
    }
    
    
    
}
