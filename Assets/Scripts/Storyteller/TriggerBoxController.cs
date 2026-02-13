using System;
using UnityEngine;

public class TriggerBox : MonoBehaviour
{
    [SerializeField] private int id;
    private void OnTriggerEnter(Collider other)
    {
        StoryEventsController.current.TriggeredEntered(id);
    }
    private void OnTriggerExit(Collider other)
    {
        StoryEventsController.current.TriggerExited(id);
    }
}
