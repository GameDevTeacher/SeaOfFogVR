using System;
using System.Collections.Generic;
using UnityEngine;

public class EchoInteractListener : MonoBehaviour
{
    public void PlayEcho(string filepath) => FmodController.current.UpdateEchoTrigger(filepath); 
}
