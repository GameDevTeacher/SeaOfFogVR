using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameInitiator : MonoBehaviour
{
    [SerializeField] private SceneField[] _scenesToLoad;
    [SerializeField] private SceneField[] _scenesToUnload;
    [SerializeField] private List<AsyncOperation> _asyncOperations =  new List<AsyncOperation>();
    private List<SceneManager> _sceneManagers = new List<SceneManager>();
    
    void Start()
    {
        StartCoroutine(LoadScenes());
        UnloadScenes();
    }
    
    public IEnumerator LoadScenes()
    {
        
        for (int i = 0; i < _scenesToLoad.Length; i++)
        {
            bool isSceneLoaded = false;
            for (int j = 0; j < SceneManager.sceneCount; j++)
            {
                Scene loadedScene = SceneManager.GetSceneAt(j);
                if (loadedScene.name == _scenesToLoad[i].SceneName)
                {
                    print($"scene at index {i} is already loaded");
                    isSceneLoaded = true;
                    break;
                }
            }
            
            if (!isSceneLoaded)
            {
                SceneManager.LoadSceneAsync(_scenesToLoad[i], LoadSceneMode.Additive);
                
                //loads scene but doesnt activate it
                // AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(_scenesToLoad[i], LoadSceneMode.Additive);
                // asyncOperation.allowSceneActivation = false;
                // _asyncOperations.Insert(i, asyncOperation); //adds scene loading operation to list
            }
            
        }

        yield return new WaitUntil(() => SceneManager.GetSceneByName(_scenesToLoad[1].SceneName).isLoaded);
        SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(1));
        SceneManager.UnloadSceneAsync(SceneManager.GetSceneByBuildIndex(0));
        
        print("scenes in list: "  + _scenesToLoad.Length);
    }
    
    public void UnloadScenes()
    {
        for (int i = 0; i < _scenesToUnload.Length; i++)
        {
            for (int j = 0; j < SceneManager.sceneCount; j++)
            {
                Scene loadedScene = SceneManager.GetSceneAt(j);
                if (loadedScene.name == _scenesToUnload[i].SceneName)
                {
                    SceneManager.UnloadSceneAsync(_scenesToUnload[i]);
                }
            }
            
        }
    }
    
    public void StartGame()
    {
        Time.timeScale = 1;
    }
}
