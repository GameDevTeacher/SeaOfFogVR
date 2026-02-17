using System;
using UnityEngine;

public class StoryEventsController : MonoBehaviour
{
  public static StoryEventsController current;


  [Header("StoryEventsCleared")]
  public bool caveEntered;
  
  private void Awake()
  {
    if (current == null)
    {
      current = this;
      DontDestroyOnLoad(gameObject);
    }
    else
    {
      Destroy(gameObject);
    }
  }
  
  #region EVENTS

  public Action onBoatArrivingFishmanIsle;

  public void BoatArrivingFishmanIsle()
  {
    onBoatArrivingFishmanIsle?.Invoke();
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

  #endregion
  
 
  


  

}
