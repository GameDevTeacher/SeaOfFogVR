using System;
using System.Collections.Generic;
using UnityEngine;

public class EchoInteractListener : MonoBehaviour
{
    [SerializeField] private string EchoFileName;
    public string[] echoes; 
    
    public void Start()
    {
        StoryEventsController.current.onEchoInteraction += PlayEcho;
        echoes = new []{"Test_echo_1",  "Test_echo_2", "Test_echo_3"};
        
    }

    
    public void PlayEcho()
    {
        foreach (var echo in echoes)
        {
            if (echo.Equals(EchoFileName, StringComparison.OrdinalIgnoreCase))
            {
                //Replace with audio later
                Debug.Log("Echo: " + echo);
            }
        }
    }

    
}
