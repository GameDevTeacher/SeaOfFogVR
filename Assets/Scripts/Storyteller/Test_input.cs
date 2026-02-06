using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test_input : MonoBehaviour
{
   private void Update()
   {
      

      if (Keyboard.current.rKey.wasPressedThisFrame)
      {
         StoryEventsController.current.EchoInteraction();
      }

      
   }
   
}
