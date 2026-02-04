using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test_input : MonoBehaviour
{
   private void Update()
   {
      if (Keyboard.current.tKey.wasPressedThisFrame)
      {
         StoryEventsController.current.InteractWithObject();
      }
      if (Keyboard.current.yKey.wasPressedThisFrame)
      {
         var countDownSeconds = 10f;
         StoryEventsController.current.CountDownEvent(countDownSeconds);
            
      }

      if (Keyboard.current.rKey.wasPressedThisFrame)
      {
         var id = 1;
         StoryEventsController.current.EchoInteraction(id);
      }
   }
   
}
