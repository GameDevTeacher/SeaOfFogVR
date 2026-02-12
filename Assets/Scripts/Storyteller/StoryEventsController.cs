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
  
  public Action<int> onTriggerEntered;
  public void TriggeredEntered(int id)
  {
    onTriggerEntered.Invoke(id);
  }
  public Action<int> onTriggerExited;
  public void TriggerExited(int id)
  {
    onTriggerExited.Invoke(id);
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
