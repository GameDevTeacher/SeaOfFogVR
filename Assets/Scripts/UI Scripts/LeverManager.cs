using UnityEngine;
using TMPro;
using System;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.Rendering.Universal;

public class LeverManager : MonoBehaviour
{
    public enum VolumeType
    {
        Master,
        Ambience,
        Music,
        Voice,
        Reverb
    }
    
    [Header("Lever")]
    [SerializeField] private HingeJoint lever;
    [SerializeField] private Rigidbody leverRigidbody;
    [SerializeField] private Transform leverTransform;
    
    [Header("Limits")]
    [SerializeField] private float maxLimit;
    [SerializeField] private float minLimit;

    [Header("Setting")] 
    [SerializeField] private float volume;
    private float _volumeMin = -80f;
    private float _volumeMax = 10f;
    
    public VolumeType volumeType;
    
    private FmodController _fmodController;
    
    private void Start()
    {
        _fmodController = FmodController.current;
        
        leverTransform.eulerAngles = new Vector3(0, leverTransform.eulerAngles.y, 0);
        
        if (lever.useLimits)
        {
            var value = Mathf.Clamp(lever.angle, minLimit, maxLimit);
            volume = NewScaleRegulator(value);
            SpecifyVolume(volumeType);
        }
    }

    private void Update()
    {
        if (leverRigidbody.angularVelocity != Vector3.zero)
        {
            var value = Mathf.Clamp(lever.angle, minLimit, maxLimit);
            volume = NewScaleRegulator(value);
            SpecifyVolume(volumeType);
        }
    }

    private void SpecifyVolume(VolumeType volType)
    {
        switch (volType)
        {
            case VolumeType.Master:
                _fmodController.masterVolume = FunTimes(volume);
                break;
            case VolumeType.Ambience:
                _fmodController.ambienceVolume = FunTimes(volume);
                break;
            case VolumeType.Music:
                _fmodController.musicVolume = FunTimes(volume);
                break;
            case VolumeType.Voice:
                _fmodController.voicelinesVolume = FunTimes(volume);
                break;
            case VolumeType.Reverb:
                _fmodController.reverbVolume = FunTimes(volume);
                break;
        }
    }
    
    private float NewScaleRegulator(float value)
    {
        return (value - minLimit) / (maxLimit - minLimit);
    }

    private float FunTimes(float value)
    {
        // Take the value from the new scale regulator and translate that into values the funtimes can understand
        return (_volumeMin - _volumeMax) * value + _volumeMax;
    }
}
