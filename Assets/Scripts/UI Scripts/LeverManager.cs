using UnityEngine;
using TMPro;
using System;

public class LeverManager : MonoBehaviour
{
    [Header("Testing")]
    [SerializeField] private TextMeshProUGUI text;
    
    [Header("Lever")]
    [SerializeField] private HingeJoint lever;
    [SerializeField] private Rigidbody leverRigidbody;
    [SerializeField] private Transform leverTransform;
    
    [Header("Limits")]
    [SerializeField] private float maxLimit;
    [SerializeField] private float minLimit;
    
    private void Start()
    {
        leverTransform.eulerAngles = new Vector3(0, leverTransform.eulerAngles.y, 0);
        
        if (lever.useLimits)
        {
            var value = Mathf.Clamp(lever.angle, minLimit, maxLimit);
            text.text = "Value: " + Math.Round(NewScaleRegulator(value), 2);
        }
    }

    private void Update()
    {
        if (leverRigidbody.angularVelocity != Vector3.zero)
        {
            var value = Mathf.Clamp(lever.angle, minLimit, maxLimit);
            print("When regulated, that value becomes: " + NewScaleRegulator(value));
            text.text = "Value: " + Math.Round(NewScaleRegulator(value), 2);
        }
    }

    private float OldScaleRegulator(float value)
    {
        return (value + 75) * 2 / 3;
    }

    private float NewScaleRegulator(float value)
    {
        return (value - minLimit) / (maxLimit - minLimit);
    }
}
