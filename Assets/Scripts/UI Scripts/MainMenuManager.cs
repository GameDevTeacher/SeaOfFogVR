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
    public bool shouldCameraFade = false;
    
    
    private VRNoPeeking _noPeekingInstance;

    private void Start()
    {
        shouldCameraFade = false;
        _noPeekingInstance = VRNoPeeking.Instance;
    }

    private void Update()
    {
        if (shouldCameraFade)
        {
            _noPeekingInstance.CameraFadeOut(1f, fadeOutSpeed);
        }
    }
    
    public void StartGame()
    {
        animator.Play(clip.name); 
        shouldCameraFade = true; 
    }
}
