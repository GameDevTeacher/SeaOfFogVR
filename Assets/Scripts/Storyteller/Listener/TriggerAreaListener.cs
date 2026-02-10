using UnityEngine;

public class TriggerAreaListener : MonoBehaviour
{
    
    void Start()
    {
        StoryEventsController.current.onTriggerEntered += TriggerEntered;
        StoryEventsController.current.onTriggerExited += TriggerExited;
        
    }

    private void TriggerEntered()
    {
        GetComponent<MeshRenderer>().material.color = Color.green;
        Debug.Log("Entered Trigger");
    }

    private void TriggerExited()
    {
        GetComponent<MeshRenderer>().material.color = Color.dodgerBlue;
        Debug.Log("Exited Trigger");
    }
}
