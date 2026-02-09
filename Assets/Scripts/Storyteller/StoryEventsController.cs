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
  
  public Action onTriggerEntered;
  public void TriggeredEntered()
  {
    onTriggerEntered.Invoke();
  }
  public Action onTriggerExited;
  public void TriggerExited()
  {
    onTriggerExited.Invoke();
  }
  
  public Action<float> onTimedTriggerStart;

  public void TimedTriggerStart(float duration)
  {
    onTimedTriggerStart.Invoke(duration);
  }
  public Action onTimedTriggerStop;

  public void TimeTriggerStop()
  {
    onTimedTriggerStop.Invoke();
  }
  


  

}
