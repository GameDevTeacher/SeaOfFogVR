using UnityEngine;
using UnityEngine.SceneManagement;

public class HoverManager : MonoBehaviour
{
    [Header("Scene Loading")]
    [SerializeField] private bool shouldSceneBeLoaded;
    
    [Header("Animation")]
    [SerializeField] private Animator animator;
    [SerializeField] private AnimationClip clip;
    
    [Header("Testing")]
    [SerializeField] private Material yesMat;
    [SerializeField] private Material noMat;
    
    public void FeelTheHover(MeshRenderer thisMesh)
    {
        print("Currently feeling the energy of the player");
        thisMesh.material = yesMat;
    }

    public void UnfeelTheHover(MeshRenderer thisMesh)
    {
        print("Unfeeling the energy of the player");
        thisMesh.material = noMat;
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
            print("Scene not loaded.");
        }
    }
}
