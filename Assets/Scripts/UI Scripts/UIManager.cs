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
    
    [Header("Testing")]
    [SerializeField] private VRNoPeeking vrNoPeeking;

    private void Start()
    {
        isPaused = false;
        pauseMenu.SetActive(false);
        _userInput = GetComponent<UserInputManager>();
    }

    private void Update()
    {
        if (_userInput.Pause && !isPaused || Keyboard.current[pauseKey].wasPressedThisFrame && !isPaused)
        {
            isPaused = true;
            print("Paused");
            vrNoPeeking.CameraFadeOut(1f, gameObject.name);
        }
        else if (_userInput.Pause && isPaused || Keyboard.current[pauseKey].wasPressedThisFrame && isPaused)
        {
            isPaused = false;
            print("Unpaused");
            vrNoPeeking.CameraFadeIn(0f, gameObject.name);
        }
    }

    public void Quit()
    {
        Application.Quit();
    }
}
