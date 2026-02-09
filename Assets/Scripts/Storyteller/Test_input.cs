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
      if (Keyboard.current.dKey.wasPressedThisFrame)
      {
         StoryEventsController.current.TimedTriggerStart(3f);
      }
      if (Keyboard.current.fKey.wasPressedThisFrame)
      {
         StoryEventsController.current.TimeTriggerStop();
      }
      

      
   }
   
}
