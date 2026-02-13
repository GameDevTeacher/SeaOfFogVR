using UnityEngine;
using TMPro;
using System;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.Rendering.Universal;

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

    [Header("Setting")] 
    [Range(0, 1)]
    [SerializeField] private float volumeTest;
    [SerializeField] private float volumeTestMax, volumeTestMin; // should be replaced by a settings value from FMOD or otherwise

    public Volume volume;
    private ColorAdjustments _colorAdjustments;
    
    private void Start()
    {
        leverTransform.eulerAngles = new Vector3(0, leverTransform.eulerAngles.y, 0);
        volume.profile.TryGet(out _colorAdjustments);
        _colorAdjustments.hueShift.max = volumeTestMax;
        _colorAdjustments.hueShift.min = volumeTestMin;
        
        if (lever.useLimits)
        {
            var value = Mathf.Clamp(lever.angle, minLimit, maxLimit);
            text.text = "Value: " + Math.Round(NewScaleRegulator(value), 2);
            volumeTest = NewScaleRegulator(value);
            _colorAdjustments.hueShift.value = volumeTest;
        }
    }

    private void Update()
    {
        if (leverRigidbody.angularVelocity != Vector3.zero)
        {
            var value = Mathf.Clamp(lever.angle, minLimit, maxLimit);
            text.text = "Value: " + Math.Round(NewScaleRegulator(value), 2);
            volumeTest = NewScaleRegulator(value);
            _colorAdjustments.hueShift.value = volumeTest;
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
