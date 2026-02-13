using UnityEngine;
using UnityEngine.Serialization;

public class EchoHighlight : MonoBehaviour
{
    [Header("Echo Shadering")]
    [SerializeField] private Renderer echoRenderer;
    private Material _echoMaterial;
    
    private void Awake() => _echoMaterial = echoRenderer.materials[1];

    public void Transparency(float targetAlpha)
    {
        var transparencyID = Shader.PropertyToID("_Transparency");
        _echoMaterial.SetFloat(transparencyID, targetAlpha);
    }

    public void WobbleWithDavid(float targetWobble)
    {
        var wobbleID = Shader.PropertyToID("_VertexOffset");
        _echoMaterial.SetFloat(wobbleID, targetWobble);
    }
}
