using UnityEngine;
using UnityEngine.InputSystem;

public class UIManager : MonoBehaviour
{
    [Header("Pausing")]
    [SerializeField] private GameObject pauseMenuPrefab;
    [SerializeField] private Transform pauseMenuSpawnPoint;
    [SerializeField] private bool isPaused;
    private GameObject _pauseMenu;
    
    [Header("Input")] 
    [SerializeField] private Key pauseKey;
    private UserInputManager _userInput;

    private void Start()
    {
        isPaused = false;
        _userInput = GetComponent<UserInputManager>();
    }

    private void Update()
    {
        if (_userInput.Pause && !isPaused || Keyboard.current[pauseKey].wasPressedThisFrame && !isPaused)
        {
            isPaused = true;
            _pauseMenu = Instantiate(pauseMenuPrefab, pauseMenuSpawnPoint.position, pauseMenuSpawnPoint.rotation);
            print("Paused");
        }
        else if (_userInput.Pause && isPaused || Keyboard.current[pauseKey].wasPressedThisFrame && isPaused)
        {
            isPaused = false;
            Destroy(_pauseMenu);
            print("Unpaused");
        }
    }

    public void Quit()
    {
        Application.Quit();
    }
}
