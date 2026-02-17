using System;
using UnityEngine;

public class CaveExitEchoes : MonoBehaviour
{
    [SerializeField] private string echoPath;
    public bool triggerEnter;
    public bool triggered;
    private void OnTriggerEnter(Collider other)
    {
        if (StoryEventsController.current.caveEntered && triggerEnter && !triggered)
        {
            FmodController.current.UpdateEchoTrigger(echoPath);
            triggerEnter = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (StoryEventsController.current.caveEntered && triggerEnter && !triggered)
        {
            FmodController.current.UpdateEchoTrigger(echoPath);
            triggerEnter = true;
        }
    }
}
