using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    // Starting with something simple, a system that loads a specific scene, without having to write a string name
    public void LoadSpecificScene(Scene sceneToLoad)
    {
        SceneManager.LoadScene(sceneToLoad.name);
    }
}
