using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

public class HoverManager : MonoBehaviour
{
    public Material yesMat, noMat;
    public SceneAsset sceneToLoad;
    
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

    public void StartGame(bool shouldSceneBeLoaded, Animator animator, AnimationClip animationClip)
    {
        if (shouldSceneBeLoaded)
        {
            SceneManager.LoadScene(sceneToLoad.name);
        }
        else
        {
            animator.Play(animationClip.name);
            print("Scene not loaded.");
        }
    }

    public void InteractWithHatch(SkinnedMeshRenderer thisMesh)
    {
        print("Interacting with hatch");
        thisMesh.material = noMat;
    }
}
