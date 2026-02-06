using UnityEngine;

public class InteractionController : MonoBehaviour
{
    public Camera playerCamera;
    
    public float interactionDistance;
    
    IInteractable _interactable;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateCurrentInteractable();
    }

    private void UpdateCurrentInteractable()
    {
        var ray = playerCamera.ViewportPointToRay(new Vector2(0.5f,0.5f),Camera.MonoOrStereoscopicEye.Right);
        
        Physics.Raycast(ray, out RaycastHit hit, interactionDistance);
        
        _interactable = hit.collider.GetComponent<IInteractable>();
    }

    private void ActivateInteraction()
    {
        _interactable?.Interact();
    }
    
}
