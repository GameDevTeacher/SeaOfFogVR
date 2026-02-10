using System;
using UnityEngine;

public class VRNoPeeking : MonoBehaviour
{
    [SerializeField] private LayerMask collisionLayer;
    [SerializeField] private float fadeOutSpeed;
    [SerializeField] private float fadeInSpeed;
    [SerializeField] private float sphereCheckSize = 0.15f;

    private Material _cameraFadeMat;
    private bool _isCameraFadedOut = false;
    
    private MainMenuManager _mainMenuManager;

    private void Start()
    { 
        _mainMenuManager = MainMenuManager.Instance;
        _cameraFadeMat = GetComponent<Renderer>().material;
    } 

    private void Update()
    {
        if (Physics.CheckSphere(transform.position, sphereCheckSize, collisionLayer, QueryTriggerInteraction.Ignore))
        {
            CameraFadeOut(1f);
            _isCameraFadedOut = true;
        }
        else if (_mainMenuManager.shouldCameraFade && !_isCameraFadedOut)
        {
            CameraFadeOut(1f);
        }
        else
        {
            if (!_isCameraFadedOut) return;
            
            CameraFadeIn(0f);
        }
    }

    public void CameraFadeOut(float targetAlpha)
    {
        var alphaName = Shader.PropertyToID("_AlphaValue");
        var fadeValue = Mathf.MoveTowards(_cameraFadeMat.GetFloat(alphaName), targetAlpha, 
            Time.deltaTime / fadeOutSpeed);
        _cameraFadeMat.SetFloat(alphaName, fadeValue);
        
        if (fadeValue <= 0.01f)
            _isCameraFadedOut = false;
    }

    public void CameraFadeIn(float targetAlpha)
    {
        var alphaName = Shader.PropertyToID("_AlphaValue");
        var fadeValue = Mathf.MoveTowards(_cameraFadeMat.GetFloat(alphaName), targetAlpha, 
            Time.deltaTime / fadeInSpeed);
        _cameraFadeMat.SetFloat(alphaName, fadeValue);
        
        if (fadeValue <= 0.01f)
            _isCameraFadedOut = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, sphereCheckSize);
    }
}
