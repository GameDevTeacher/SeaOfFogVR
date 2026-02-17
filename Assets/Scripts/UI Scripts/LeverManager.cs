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
        
        
        print(name + volumeType);
        #region lever Positioner
        float volValue = 0f;
        switch (volumeType)
        {
            case VolumeType.Master:
                _fmodController.masterVolume = PlayerPrefs.GetFloat(volumeType.ToString(), volume)*90-80;
                volValue = (_fmodController.masterVolume +80)/90;
                break;
            case VolumeType.Ambience:
                _fmodController.ambienceVolume = PlayerPrefs.GetFloat(volumeType.ToString(), volume)*90-80;
                volValue = (_fmodController.ambienceVolume +80)/90;
                break;
            case VolumeType.Music:
                _fmodController.musicVolume = PlayerPrefs.GetFloat(volumeType.ToString(), volume)*90-80;
                volValue = (_fmodController.musicVolume +80)/90;
                break;
            case VolumeType.Voice:
                _fmodController.voicelinesVolume =  PlayerPrefs.GetFloat(volumeType.ToString(), volume)*90-80;
                volValue = (_fmodController.voicelinesVolume +80)/90;
                break;
            case VolumeType.Reverb:
                _fmodController.reverbVolume = PlayerPrefs.GetFloat(volumeType.ToString(), volume)*90-80;
                volValue = (_fmodController.reverbVolume +80)/90;
                break;
        }
        volValue = (volValue * (Mathf.Abs(minLimit)+maxLimit)-(Mathf.Abs(minLimit)+maxLimit)/2)*-1; //traslates the previous value to get a rotational value based on the clamp limits
        if (float.IsNaN(volValue))
        {
            volValue = 0.5f;
            Debug.LogWarning(name + "NaN, setting: " + volValue);
        }
        print($"leverRotation {volValue}");
        leverTransform.localRotation = Quaternion.Euler(volValue, leverTransform.eulerAngles.y, leverTransform.eulerAngles.z);
        print("please work :(" + leverTransform.localRotation.eulerAngles);
        #endregion
        
        
        
        // if (lever.useLimits)
        // {
        //     var value = Mathf.Clamp(lever.angle, minLimit, maxLimit);
        //     volume = NewScaleRegulator(value);
        //     SpecifyVolume(volumeType);
        // }
        leverRigidbody.angularVelocity = Vector3.zero;
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
        PlayerPrefs.SetFloat(volumeType.ToString(), volume*-1+1);
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
