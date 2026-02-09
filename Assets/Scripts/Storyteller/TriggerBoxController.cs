using System;
using UnityEngine;

public class TriggerBox : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        StoryEventsController.current.TriggeredEntered();
    }
    private void OnTriggerExit(Collider other)
    {
        StoryEventsController.current.TriggerExited();
    }
}
