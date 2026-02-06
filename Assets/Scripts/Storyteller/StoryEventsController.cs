using System;
using UnityEngine;

public class StoryEventsController : MonoBehaviour
{
  public static StoryEventsController current;
 
  private void Awake()
  {
    current = this;
    
  }
  
  public Action onEchoInteraction;
  public void EchoInteraction()
  {
    onEchoInteraction.Invoke();
  }
  
  



  

}
