using UnityEngine;
using UnityEngine.SceneManagement;


/// <summary>
/// A collection of functions used in the main menu of Havtåke VR. (apart from Lever functions)
/// </summary>
public class MainMenuManager : MonoBehaviour
{
    [Header("Fading")]
    [SerializeField] private float fadeSpeed; // The speed at which the camera fades to black.
    [SerializeField] private Renderer overlayRenderer;
    private Material _fadeMat; // The material used when fading to the next scene.
    
    [Header("Hovering")]
    [SerializeField] private Renderer hoveringRenderer;
    private Material _hoverMat; // The material used when the player hovers over an object.
    
    [Header("Trapdoor")]
    [SerializeField] private Animator animator;
    [SerializeField] private AnimationClip clip;
    [SerializeField] private bool shouldSceneBeLoaded = false;

    private void Awake()
    {
        _fadeMat = overlayRenderer.material;
        _hoverMat = hoveringRenderer.material;
    }
    
    public void StartGame(string sceneName)
    {
        if (shouldSceneBeLoaded)
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            animator.Play(clip.name);
            CameraFade(1f);
            print("Scene not loaded.");
        }
    }

    public void CameraFade(float targetAlpha)
    {
        var fadeValue = Mathf.MoveTowards(_fadeMat.GetFloat("_AlphaValue"), targetAlpha, 
            Time.deltaTime * fadeSpeed);
        _fadeMat.SetFloat("_AlphaValue", fadeValue);
    }
}
