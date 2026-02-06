using System;
using System.Collections.Generic;
using UnityEngine;

public class EchoInteractListener : MonoBehaviour
{
    [SerializeField] private string EchoFileName;
    public string[] echoes;
    private Material _materialColour;
    
    public void Start()
    {
        StoryEventsController.current.onEchoInteraction += PlayEcho;
        echoes = new []{"Test_echo_1",  "Test_echo_2", "Test_echo_3"};
        _materialColour = GetComponent<MeshRenderer>().material;
    }

    public void PlayEcho()
    {
        foreach (var echo in echoes)
        {
            if (echo.Equals(EchoFileName, StringComparison.OrdinalIgnoreCase))
            {
                Debug.Log("Echo: " + echo);
                _materialColour.color = Color.chartreuse;
                
            }
        }
    }

    
}
