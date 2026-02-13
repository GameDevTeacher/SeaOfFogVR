using System;
using UnityEngine;

public class TimedTriggerListener : MonoBehaviour
{
    private float _duration;
    private bool _triggerExited;
    private float _remainingDuration;
    private bool _isTimerDone;
    [SerializeField] private string _boatTalk;
    private CountDownTimer _countDownTimer;
    public string[] TalkInBoatStrings;
    private Material _materialColour;
    
    void Start()
    {
        StoryEventsController.current.onTimedTriggerStart += TimedTrigger;
        StoryEventsController.current.onTimedTriggerStop += triggerExited;
        _materialColour = GetComponent<MeshRenderer>().material;
    }
    
    
    public void TimedTrigger(float duration)
    {
        _triggerExited = false;
        
        _duration = duration;
        _countDownTimer = CountDownTimer.Start(duration);
        Debug.Log(duration + "in time trigger");
        
        _materialColour.color = Color.darkMagenta;
    }

    private void triggerExited()
    {
        _triggerExited = true;
        _materialColour.color = Color.deepSkyBlue;
        _countDownTimer = CountDownTimer.Start(0);
    }
    
    private void UpdateEvent()
    {
        foreach (var boatTalk in TalkInBoatStrings)
        {
            if (boatTalk.Equals(_boatTalk, StringComparison.OrdinalIgnoreCase))
            {
                //Replace with boat Talk audio later
                _materialColour.color = Color.chartreuse;
                Debug.Log("boatTalk: " + boatTalk);
            }
        }
    }
    private void Update()
    {
        if (_countDownTimer.IsCompleted && !_isTimerDone)
        {
            UpdateEvent();
            Debug.Log("Timer Is Done");
            _isTimerDone = true;
        }
    }
}
