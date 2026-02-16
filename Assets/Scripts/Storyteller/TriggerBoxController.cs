using System;
using UnityEngine;

public class TriggerBox : MonoBehaviour
{
    [SerializeField] private int id;
    [SerializeField] private BoxCollider _collider;
    

    public int ID
    {
        get => id;
    }

    private void OnTriggerEnter(Collider other)
    {
        StoryEventsController.current.TriggeredEntered(id);
    }
    private void OnTriggerExit(Collider other)
    {
        StoryEventsController.current.TriggerExited(id);
    }

    private void OnDrawGizmos()
    {
        _collider = GetComponent<BoxCollider>();
        Gizmos.color = Color.chartreuse;
        Gizmos.DrawWireCube(_collider.bounds.center, _collider.size);
    }
}
