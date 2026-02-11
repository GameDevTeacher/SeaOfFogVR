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
    }

    public static void Load()
    {
        string saveContent = File.ReadAllText(SaveFileName());
        _saveData = JsonUtility.FromJson<SaveData>(saveContent);
        HandleLoadData();
    }

    private static void HandleLoadData()
    {
        SaveManager.instance.player.Load(_saveData.playerData);
        SaveManager.instance.boat.Load(_saveData.boatData);
    }
    
    
    
}
