using System;
using UnityEngine;

public class VRNoPeeking : MonoBehaviour
{
    public static VRNoPeeking Instance;
    
    [Header("Fading")]
    [SerializeField] private LayerMask collisionLayer;
    public float defaultFadeOutSpeed;
    [SerializeField] private float fadeInSpeed;
    [SerializeField] private float sphereCheckSize = 0.15f;
    
    [Header("Shader IDs")]
    [SerializeField] private int alphaName;
    
    [Header("Camera")]
    [SerializeField] private Transform cameraTransform;

    private Material _cameraFadeMat;
    private bool _isCameraFadedOut = false;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    { 
        _cameraFadeMat = GetComponent<Renderer>().material;
        
        alphaName = Shader.PropertyToID("_AlphaValue");
        
        CameraFadeIn(0f);
    } 

    private void Update()
    {
        if (Physics.CheckSphere(cameraTransform.position, sphereCheckSize, collisionLayer, QueryTriggerInteraction.Ignore))
        {
            CameraFadeOut(1f, defaultFadeOutSpeed);
            _isCameraFadedOut = true;
        }
        else
        {
            if (!_isCameraFadedOut) return;
            
            CameraFadeIn(0f);
        }
    }

    public void CameraFadeOut(float targetAlpha, float fadeOutSpeed)
    {
        var fadeValue = Mathf.MoveTowards(_cameraFadeMat.GetFloat(alphaName), targetAlpha, 
            Time.deltaTime / fadeOutSpeed);
        _cameraFadeMat.SetFloat(alphaName, fadeValue);
        
        if (fadeValue <= 0.01f)
            _isCameraFadedOut = false;
    }

    public void CameraFadeIn(float targetAlpha)
    {
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
