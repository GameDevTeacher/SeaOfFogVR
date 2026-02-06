using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

public class HoverManager : MonoBehaviour
{
    public Material yesMat, noMat;
    //public SceneAsset sceneToLoad;
    public Animator animator;
    public AnimationClip clip;
    
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

    /*public void StartGame(bool shouldSceneBeLoaded)
    {
        if (shouldSceneBeLoaded)
        {
            SceneManager.LoadScene(sceneToLoad.name);
        }
        else
        {
            animator.Play(clip.name);
            print("Scene not loaded.");
        }
    }*/
}
