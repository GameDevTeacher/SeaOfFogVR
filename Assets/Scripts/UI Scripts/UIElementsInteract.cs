using UnityEngine;

public class UIElementsInteract : MonoBehaviour
{
    [Header("Hovering")]
    [SerializeField] private Renderer hoveringRenderer;
    private Material _hoverMat; // The material used when the player hovers over an object.
    
    private void Awake() => _hoverMat = hoveringRenderer.materials[1];
    
    public void Highlight(float targetHighlight)
    {
        var highlightName = Shader.PropertyToID("_Transparency");
        _hoverMat.SetFloat(highlightName, targetHighlight);
    }

    public void WibbleWobble(float targetWobble)
    {
        var wobbleName = Shader.PropertyToID("_VertexOffset");
        _hoverMat.SetFloat(wobbleName, targetWobble);
    }
}
