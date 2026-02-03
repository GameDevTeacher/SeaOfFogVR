using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [Header("Start Game")]
    [SerializeField] private SceneAsset sceneToLoad;
    
    [Header("Pausing")]
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private bool isPaused;
    
    [Header("Input")] 
    [SerializeField] private Key pauseKey;
    
    [Header("Marmine Fun")]
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip clip;

    private void Start()
    {
        isPaused = false;
        pauseMenu.SetActive(false);
    }

    private void Update()
    {
        // TODO: add input for pausing, using a keyboard layout at first.
        if (Keyboard.current[pauseKey].wasPressedThisFrame && !isPaused)
        {
            pauseMenu.SetActive(true);
            isPaused = true;
            Time.timeScale = 0;
        }
        else if (Keyboard.current[pauseKey].wasPressedThisFrame && isPaused) 
        {
            pauseMenu.SetActive(false);
            isPaused = false;
            Time.timeScale = 1;
        }
    }
    
    // Starting with something simple, a system that loads a specific scene, without having to write a string name
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

    public void Quit()
    {
        Application.Quit();
    }

    public void PlaySomeNoise()
    {
        source.PlayOneShot(clip);
    }
}
