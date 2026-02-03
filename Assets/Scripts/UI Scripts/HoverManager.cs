using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

public class HoverManager : MonoBehaviour
{
    public Material yesMat, noMat;
    public MeshRenderer trapDoorRenderer;
    public SceneAsset sceneToLoad;
    
    public void FeelTheHover()
    {
        print("Currently feeling the energy of the player");
        trapDoorRenderer.material = yesMat;
    }

    public void UnfeelTheHover()
    {
        print("Unfeeling the energy of the player");
        trapDoorRenderer.material = noMat;
    }

    public void StartGame(bool shouldSceneBeLoaded)
    {
        if (shouldSceneBeLoaded)
        {
            SceneManager.LoadScene(sceneToLoad.name);
        }
        else
        {
            print("Scene not loaded.");
        }
    }
}
