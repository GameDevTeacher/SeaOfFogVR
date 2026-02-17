using UnityEngine;

public class LanternEchoInteract : MonoBehaviour
{
    [Header("Casting")] 
    [SerializeField] private Transform startPoint;
    [SerializeField] private float radius;
    [SerializeField] private float maxDistance;
    
    [Header("Visualize Interaction")]
    [SerializeField] private Renderer visualizeRenderer;
    
    private Transform _currentTarget;
    private EchoHighlight _echoHighlightTarget;

    private void Update()
    {
        RaycastHit hit;
        
        if (Physics.SphereCast(startPoint.position, radius, Vector3.right, out hit, maxDistance))
        {
            if (hit.collider.CompareTag("Echo"))
            {
                print("I see you.");
                _currentTarget = hit.collider.transform;
                _currentTarget.TryGetComponent(out EchoHighlight echoHighlight);
                    _echoHighlightTarget = echoHighlight;
                    
                _echoHighlightTarget.Transparency(0.5f);
                _echoHighlightTarget.WobbleWithDavid(0.2f);
            }
        }
        else
        {
            if (_currentTarget == null) return;
            
            _currentTarget.TryGetComponent(out EchoHighlight echoHighlight);
                _echoHighlightTarget = echoHighlight;
                
            _echoHighlightTarget.Transparency(0);
            _echoHighlightTarget.WobbleWithDavid(0);
            
            _currentTarget = null;
            _echoHighlightTarget = null;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.aquamarine;
        Gizmos.DrawWireSphere(startPoint.position, radius);
    }
    
}
