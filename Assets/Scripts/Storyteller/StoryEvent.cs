using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu (menuName = "Storyteller/StoryEvent")]
public class StoryEvent : ScriptableObject
{
    public List<StoryEventListener> listeners = new List<StoryEventListener>();

    public void Raise()
    {
        
    }

    public void RegisterListener(StoryEventListener listener)
    {
        if (!listeners.Contains(listener))
        {
            listeners.Add(listener);
        }
    }

    public void UnregisterListener(StoryEventListener listener)
    {
        if (listeners.Contains(listener))
        {
            listeners.Remove(listener);
        }
    }
}

