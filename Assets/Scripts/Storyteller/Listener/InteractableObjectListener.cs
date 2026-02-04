using UnityEngine;

public class InteractableObjectListener : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        StoryEventsController.current.onInteractWithObject += InteractWithObject;
    }

    private void InteractWithObject()
    {
        Debug.Log("You interacted with the InteractableObjectListener");
    }
 
    
}
