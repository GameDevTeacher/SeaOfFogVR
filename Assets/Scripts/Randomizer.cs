using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;
using Random = UnityEngine.Random;

public class Randomizer : MonoBehaviour
{
    public UnityEvent<float> balls;
    public float rngMax;
    public float rngMin;
    public float rngTimerMax;
    public float rngTimerMin;
    private float _rngTimer;
    public float defaultValue;

    void Update()
    {
        if (_rngTimer <= 0)
        {
            _rngTimer = Random.Range(rngTimerMin, rngTimerMax);
            balls.Invoke(defaultValue + Random.Range(rngMin, rngMax));
        }
        else
        {
            _rngTimer -= Time.deltaTime;
        }
        
    }


}
