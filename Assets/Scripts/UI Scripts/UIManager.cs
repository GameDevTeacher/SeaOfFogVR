using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [Header("Pausing")]
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private bool isPaused;
    
    [Header("Input")] 
    [SerializeField] private Key pauseKey;

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
        
        if (Keyboard.current[pauseKey].wasReleasedThisFrame && isPaused) 
        {
            pauseMenu.SetActive(false);
            isPaused = false;
            Time.timeScale = 1;
        }
    }
    
    // Starting with something simple, a system that loads a specific scene, without having to write a string name
    public void LoadSpecificScene(Scene sceneToLoad)
    {
        SceneManager.LoadScene(sceneToLoad.name);
    }
}
