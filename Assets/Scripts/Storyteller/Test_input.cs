using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test_input : MonoBehaviour
{
   private void Update()
   {
      

      if (Keyboard.current.rKey.wasPressedThisFrame)
      {
         var id = 1;
         StoryEventsController.current.EchoInteraction();
      }

      
   }
   
}
