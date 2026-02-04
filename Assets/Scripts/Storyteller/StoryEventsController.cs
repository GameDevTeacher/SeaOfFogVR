using System;
using UnityEngine;

public class StoryEventsController : MonoBehaviour
{
  public static StoryEventsController current;

  private void Awake()
  {
    current = this;
  }

  public event Action onInteractWithObject;
  public void InteractWithObject()
  {
    onInteractWithObject?.Invoke();
  }

  public event Action<float> onCountDownFinished;
    public void CountDownEvent(float seconds)
  {
    onCountDownFinished?.Invoke(seconds);
  }

  public event Action<int> onEchoInteraction;
  public void EchoInteraction(int id)
  {
    onEchoInteraction?.Invoke(id);
  }

}
