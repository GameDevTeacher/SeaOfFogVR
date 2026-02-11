using System;
using UnityEngine;

public class VRNoPeeking : MonoBehaviour
{
    [Header("Fading")]
    [SerializeField] private LayerMask collisionLayer;
    [SerializeField] private float fadeOutSpeed;
    [SerializeField] private float fadeInSpeed;
    [SerializeField] private float sphereCheckSize = 0.15f;
    
    [Header("Shader IDs")]
    [SerializeField] private int alphaName;
    
    [Header("Camera")]
    [SerializeField] private Transform cameraTransform;

    private Material _cameraFadeMat;
    private bool _isCameraFadedOut = false;
    
    private MainMenuManager _mainMenuManager;

    private void Start()
    { 
        _mainMenuManager = MainMenuManager.Instance;
        _cameraFadeMat = GetComponent<Renderer>().material;
        
        alphaName = Shader.PropertyToID("_AlphaValue");
    } 

    private void Update()
    {
        if (Physics.CheckSphere(cameraTransform.position, sphereCheckSize, collisionLayer, QueryTriggerInteraction.Ignore))
        {
            CameraFadeOut(1f, gameObject.name);
            _isCameraFadedOut = true;
        }
        else if (_mainMenuManager.shouldCameraFade && !_isCameraFadedOut)
        {
            CameraFadeOut(1f, gameObject.name);
        }
        else
        {
            if (!_isCameraFadedOut) return;
            
            CameraFadeIn(0f, gameObject.name);
        }
    }

    public void CameraFadeOut(float targetAlpha, string caller)
    {
        print("Fade out was called by: " + caller);
        
        var fadeValue = Mathf.MoveTowards(_cameraFadeMat.GetFloat(alphaName), targetAlpha, 
            Time.deltaTime / fadeOutSpeed);
        _cameraFadeMat.SetFloat(alphaName, fadeValue);
        
        if (fadeValue <= 0.01f)
            _isCameraFadedOut = false;
    }

    public void CameraFadeIn(float targetAlpha, string caller)
    {
        print("Fade in was called by " + caller);
        
        var fadeValue = Mathf.MoveTowards(_cameraFadeMat.GetFloat(alphaName), targetAlpha, 
            Time.deltaTime / fadeInSpeed);
        _cameraFadeMat.SetFloat(alphaName, fadeValue);
        
        if (fadeValue <= 0.01f)
            _isCameraFadedOut = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(cameraTransform.position, sphereCheckSize);
    }
}
