using UnityEngine;
using FMODUnity;
public class TriggerAreaListener : MonoBehaviour
{
    [SerializeField] private string _enterEchoPath;
    [SerializeField] private string _exitEchoPath;
    [SerializeField] private bool _triggered;    
    
    void Start()
    {
        //StoryEventsController.current.onTriggerEntered += TriggerEntered;
        //StoryEventsController.current.onTriggerExited += TriggerExited;
        
    }
    private void OnTriggerEnter(Collider other)
    {
        TriggerEntered();
    }
    private void OnTriggerExit(Collider other)
    { 
        TriggerExited();
    }
    private void TriggerEntered()
    {
        if ( _triggered) return;
        _triggered = true;
        GetComponent<MeshRenderer>().material.color = Color.green;
        if (_enterEchoPath == null) return;
        
        FmodController.current.UpdateEchoTrigger(_enterEchoPath); 
        
        Debug.Log("Entered Trigger");
    }

    private void TriggerExited()
    {
        if ( _triggered) return;
        _triggered = true;
        GetComponent<MeshRenderer>().material.color = Color.dodgerBlue;
        if (_exitEchoPath == null) return;
        FmodController.current.UpdateEchoTrigger(_exitEchoPath); 
        
        Debug.Log("Exited Trigger");
    }
}
