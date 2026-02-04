using System;
using UnityEngine;

public class CountdownEventListener : MonoBehaviour
{

   private void Start()
   {
      StoryEventsController.current.onCountDownFinished += UpdateCountDown;
      
   }

   private void UpdateCountDown(float seconds)
   {
      
      Debug.Log(seconds + 10f);
   }

  
}

