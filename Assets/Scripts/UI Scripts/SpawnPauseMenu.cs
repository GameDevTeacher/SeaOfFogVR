using UnityEngine;

public class SpawnPauseMenu : MonoBehaviour
{
    [Header("Pausing")]
    [SerializeField] private GameObject pauseMenuPrefab;
    [SerializeField] private PauseScrub pauseScrub;
    private GameObject _pauseMenu;
    
    
    private UserInputManager _userInput;

    private void Start()
    {
        pauseScrub.isPaused = false;
        _userInput = GetComponent<UserInputManager>();
    }

    private void Update()
    {
        if (_userInput.Pause && !pauseScrub.isPaused)
        {
            print("I am paused.");
            pauseScrub.isPaused = true;
            _pauseMenu = Instantiate(pauseMenuPrefab, transform.position, transform.rotation);
        }
        else if (_userInput.Pause && pauseScrub.isPaused && _pauseMenu != null)
        {
            pauseScrub.isPaused = false;
            Destroy(_pauseMenu);
        }
    }
}
