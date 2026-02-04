using UnityEngine;

public readonly struct  Timer
{
    public static Timer Start(float duration) => new(duration);
    public bool IsCompleted => Time.time >= _triggerTime && _triggerTime != 0;
    
    private readonly float _triggerTime;
    private Timer(float duration) => _triggerTime = Time.time + duration;

}
