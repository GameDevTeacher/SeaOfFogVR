using System;
using UnityEngine;

public class SceneData : MonoBehaviour
{

    public SceneDataScrub Data;

    private void Awake()
    {
        SaveManager.instance.sceneData.Add(this); //multi scene saving
    }

    private void OnDisable() //multi scene saving
    {
        SaveManager.instance.sceneData.Remove(this);
    }
    
    
    public void Save(ref SceneSaveData data)
    {
        data.sceneID = Data.uniqueName;
        data.sceneIndex = Data.sceneIndex;
    }

    public void Load(SceneSaveData data)
    {
        SaveManager.instance.sceneLoader.LoadSceneByIndex(data.sceneIndex);
    }
    
}

[System.Serializable]
public struct SceneSaveData
{
    public string sceneID;
    public int sceneIndex;
}

