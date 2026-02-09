using System;
using UnityEngine;

public class VRNoPeeking : MonoBehaviour
{
    [SerializeField] private LayerMask collisionLayer;
    [SerializeField] private float fadeSpeed;
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
            CameraFade(1f);
            _isCameraFadedOut = true;
        }
        else if (_mainMenuManager.shouldCameraFade && !_isCameraFadedOut)
        {
            CameraFade(1f);
        }
        else
        {
            if (!_isCameraFadedOut) return;
            
            CameraFade(0f);
        }
    }

    public void CameraFade(float targetAlpha)
    {
        var alphaName = Shader.PropertyToID("_AlphaValue");
        var fadeValue = Mathf.MoveTowards(_cameraFadeMat.GetFloat(alphaName), targetAlpha, 
            Time.deltaTime * fadeSpeed);
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
