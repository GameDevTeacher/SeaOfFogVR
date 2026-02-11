using System;
using UnityEngine;

public class SceneData : MonoBehaviour
{

    public SceneDataScrub Data;

    private void Awake()
    {
        SaveManager.instance.sceneData = this;
    }

    public void Save(ref SceneSaveData data)
    {
        data.sceneID = Data.uniqueName;
    }

    public void Load(SceneSaveData data)
    {
        SaveManager.instance.sceneLoader.LoadSceneByIndex(data.sceneID);
    }
    
}

[System.Serializable]
public struct SceneSaveData
{
    public string sceneID;
}

