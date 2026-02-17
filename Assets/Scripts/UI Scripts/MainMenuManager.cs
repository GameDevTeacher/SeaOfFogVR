using UI_Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;


/// <summary>
/// A collection of functions used in the main menu of Havtåke VR. (apart from Lever functions)
/// </summary>
public class MainMenuManager : MonoBehaviour
{
    [Header("Trapdoor")]
    [SerializeField] private Animator animator;
    [SerializeField] private AnimationClip clip;
    [SerializeField] private float fadeOutSpeed;
    private SceneLoadTrigger _sceneLoader;
    private PlayerPositionManager _playerPositionManager;
    
    
    private VRNoPeeking _noPeekingInstance;

    private void Start()
    {
        _noPeekingInstance = VRNoPeeking.Instance;
        _sceneLoader = GetComponent<SceneLoadTrigger>();
        _playerPositionManager = GetComponent<PlayerPositionManager>();
    }
    
    public async void StartGame()
    {
        animator.Play(clip.name);
        OutOfBounds.Instance.enabled = false;
        await Awaitable.WaitForSecondsAsync(1);
        _noPeekingInstance.CameraFadeOut(fadeOutSpeed);
        await Awaitable.WaitForSecondsAsync(1);
        _sceneLoader.LoadScenes();
        _playerPositionManager.TeleportGame();
        _sceneLoader.UnloadScenes();
        await Awaitable.WaitForSecondsAsync(1);
        _noPeekingInstance.CameraFadeIn();
        OutOfBounds.Instance.enabled = true;
    }
}
