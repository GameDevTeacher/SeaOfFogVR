using UnityEngine;
using UnityEngine.SceneManagement;


/// <summary>
/// A collection of functions used in the main menu of Havtåke VR. (apart from Lever functions)
/// </summary>
public class MainMenuManager : MonoBehaviour
{
    public static MainMenuManager Instance;
    
    [Header("Trapdoor")]
    [SerializeField] private Animator animator;
    [SerializeField] private AnimationClip clip;
    [SerializeField] private bool shouldSceneBeLoaded = false;
    public bool shouldCameraFade = false;

    private void Awake() => Instance = this;
    
    public void StartGame(string sceneName)
    {
        if (shouldSceneBeLoaded)
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            animator.Play(clip.name);
            shouldCameraFade = true;
            print("Scene not loaded.");
        }
    }
}
