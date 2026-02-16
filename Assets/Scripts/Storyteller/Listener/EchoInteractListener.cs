using System;
using System.Collections.Generic;
using UnityEngine;

public class EchoInteractListener : MonoBehaviour
{
    public bool triggered;
    public void PlayEcho(string filepath)
    {
        if (!triggered)
        {
            triggered = true;
            FmodController.current.UpdateEchoTrigger(filepath);
        }
    }
}
