using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class SpriteLightManager : MonoBehaviour
{
    public static SpriteLightManager instance;
    
    public List<GameObject> lights = new List<GameObject>();
    private SpriteLight _lightScript;
    [FormerlySerializedAs("LightIndex")] public int lightIndex = 0;
    
    void Start()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
        
        GameObject[] allLights = GameObject.FindGameObjectsWithTag("Light");
            
        lights.AddRange(allLights);
        lights.Reverse();
        FadeInLight();
    }

    public void UpdateSpriteScript()
    {   
        if (lightIndex >= lights.Count-1) Debug.LogWarning("SpriteLightManager: reached the end of the sprite list");
        else
        {
            lightIndex++;
            FadeInLight();
        }

    }

    private void FadeInLight()
    {
        _lightScript =  lights[lightIndex].gameObject.GetComponent<SpriteLight>();
        _lightScript.SpriteFadeIn();
    }
    
    void Update()
    {
        
    }
}
