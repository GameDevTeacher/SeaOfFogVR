using UnityEngine;

public readonly struct  CountDownTimer
{
    
    public static CountDownTimer Start(float duration) => new(duration);
    public bool IsCompleted => Time.time >= _triggerTime && _triggerTime != 0;
    
    private readonly float _triggerTime;
    private CountDownTimer(float duration) => _triggerTime = Time.time + duration;
    
    

}


