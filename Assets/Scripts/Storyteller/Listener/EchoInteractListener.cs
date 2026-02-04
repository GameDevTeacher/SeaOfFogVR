using System;
using UnityEngine;

public class EchoInteractListener : MonoBehaviour
{
    [SerializeField] private int _objectId;
    private void Start()
    {
        StoryEventsController.current.onEchoInteraction += PlayEcho;
    }

    private void PlayEcho(int id)
    {
        if (_objectId == id)
        {
            Debug.Log("Echo 1");
        }
        else if (_objectId == id - 1)
        {
            Debug.Log("Echo");
        }
    }
}
