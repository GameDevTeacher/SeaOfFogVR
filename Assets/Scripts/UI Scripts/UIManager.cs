using UnityEngine;
using UnityEngine.InputSystem;

public class UIManager : MonoBehaviour
{
    [Header("Pausing")]
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private bool isPaused;
    
    [Header("Input")] 
    [SerializeField] private Key pauseKey;
    private UserInputManager _userInput;
    
    [Header("Marmine Fun")]
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip clip;

    private void Start()
    {
        isPaused = false;
        pauseMenu.SetActive(false);
        _userInput = GetComponent<UserInputManager>();
    }

    private void Update()
    {
        // TODO: add input for pausing, using a keyboard layout at first.
        if (Keyboard.current[pauseKey].wasPressedThisFrame && !isPaused || _userInput.Pause && !isPaused)
        {
            pauseMenu.SetActive(true);
            isPaused = true;
            Time.timeScale = 0;
        }
        else if (Keyboard.current[pauseKey].wasPressedThisFrame && isPaused || _userInput.Pause && isPaused) 
        {
            pauseMenu.SetActive(false);
            isPaused = false;
            Time.timeScale = 1;
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
