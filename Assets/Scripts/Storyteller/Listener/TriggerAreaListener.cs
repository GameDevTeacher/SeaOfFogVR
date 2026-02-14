using UnityEngine;

public class TriggerAreaListener : MonoBehaviour
{
    [SerializeField] private string _enterEchoPath;
    [SerializeField] private string _exitEchoPath;
    
    
    void Start()
    {
        StoryEventsController.current.onTriggerEntered += TriggerEntered;
        StoryEventsController.current.onTriggerExited += TriggerExited;
        
    }

    private void TriggerEntered(int id)
    {
        if (id != GetComponent<TriggerBox>().ID) return;
        GetComponent<MeshRenderer>().material.color = Color.green;
        if (_enterEchoPath == null) return;
        
        FmodController.current.UpdateEchoTrigger(_enterEchoPath); 
        Debug.Log("Entered Trigger");
    }

    private void TriggerExited(int id)
    {
        if (id != GetComponent<TriggerBox>().ID) return;
        GetComponent<MeshRenderer>().material.color = Color.dodgerBlue;
        if (_exitEchoPath == null) return;
        FmodController.current.UpdateEchoTrigger(_exitEchoPath); 
        Debug.Log("Exited Trigger");
    }
}
