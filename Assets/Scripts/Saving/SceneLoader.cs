using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    #region  SingleSceneLoader
    /*
    [SerializeField] private SceneDataScrub[] _sceneDataScrubArray;
    private Dictionary<string, int> _sceneIDToIndexMap = new Dictionary<string, int>();
    
    private void Awake()
    {
        SaveManager.instance.sceneLoader = this;
    
        PopulateSceneMappings();
    }
    
    private void PopulateSceneMappings()
    {
        foreach (var sceneDataScrub in _sceneDataScrubArray)
        {
            _sceneIDToIndexMap[sceneDataScrub.uniqueName] = sceneDataScrub.sceneIndex;
        }
    }
    
    public void LoadSceneByIndex(string savedSceneID)
    {
        if (_sceneIDToIndexMap.TryGetValue(savedSceneID, out int sceneIndex))
        {
            SceneManager.LoadScene(sceneIndex);
        }
        else
        {
            Debug.LogError($"no Scene found for ID: {savedSceneID}");
        }
    }
    */
    #endregion
    
    #region  MultiSceneLoader
    [SerializeField] private SceneDataScrub[] _sceneDataScrubArray;
    private Dictionary<string, int> _sceneIDToIndexMap = new Dictionary<string, int>();

    private void Awake()
    {
        if (!SaveManager.instance.sceneLoader) SaveManager.instance.sceneLoader = this;

        PopulateSceneMappings();
    }

    private void PopulateSceneMappings()
    {
        foreach (var sceneDataScrub in _sceneDataScrubArray)
        {
            _sceneIDToIndexMap[sceneDataScrub.uniqueName] = sceneDataScrub.sceneIndex;
        }
    }

    public void LoadSceneByIndex(int sceneIndex)
    {
        if (!SceneManager.GetSceneByBuildIndex(sceneIndex).isLoaded)
        {
            print("loading scene: " + sceneIndex);
            SceneManager.LoadSceneAsync(sceneIndex, LoadSceneMode.Additive);
        }
        else
        {
            Debug.Log("scene" + sceneIndex + " already loaded");
        }
    }
    
    public void LoadSceneByIndexSingle(int sceneIndex)
    {
        print("loading scene: " + sceneIndex);
        SceneManager.LoadScene(sceneIndex);
    }
    
    #endregion
}
