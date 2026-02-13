using UnityEngine;

public class LanternEchoInteract : MonoBehaviour
{
    [Header("Casting")]
    [SerializeField] private Transform endPoint;
    [SerializeField] private float radius;
    [SerializeField] private float maxDistance;
    
    [Header("Visualize Interaction")]
    [SerializeField] private Renderer visualizeRenderer;

    private void Update()
    {
        RaycastHit hit;
        
        if (Physics.CapsuleCast(transform.position, endPoint.position, radius, transform.forward, out hit,
                maxDistance))
        {
            if (hit.collider.CompareTag("Echo"))
            {
                hit.collider.TryGetComponent(out EchoHighlight echoHighlight);
                echoHighlight.Transparency(0.5f);
                echoHighlight.WobbleWithDavid(0.2f);
            }
        }
        else
        {
            print("No nothing.");
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.aquamarine;
        Gizmos.DrawWireSphere(endPoint.position, radius);
    }
    
}
