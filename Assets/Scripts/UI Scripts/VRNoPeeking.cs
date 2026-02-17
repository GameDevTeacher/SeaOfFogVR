using System;
using System.Collections;
using UnityEngine;

public class VRNoPeeking : MonoBehaviour
{
    public static VRNoPeeking Instance;
    
    public AnimationCurve fadeToBlackCurve;
    public AnimationCurve fadeFromBlackCurve;
    private float _curveEval;
    
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

    private bool _fadingtoBlack;
    private bool _fadingFromBlack;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    { 
        _cameraFadeMat = GetComponent<Renderer>().material;
        
        alphaName = Shader.PropertyToID("_AlphaValue");
        
        CameraFadeIn();
    } 

    private void Update()
    {
        if (Physics.CheckSphere(cameraTransform.position, sphereCheckSize, collisionLayer, QueryTriggerInteraction.Ignore))
        {
            CameraFadeOut(defaultFadeOutSpeed);
            _isCameraFadedOut = true;
        }
        else
        {
            if (!_isCameraFadedOut) return;
            
            CameraFadeIn();
        }
    }

    public async void CameraFadeOut(float fadeOutSpeed) //fade to black
    {
        _fadingtoBlack = true;
        if (_fadingFromBlack) _fadingFromBlack = false;
        print("CameraFadeOut" + fadeToBlackCurve.length);
        _curveEval = fadeFromBlackCurve.Evaluate(0);
        float timer = 0;
        while (timer <= 1)
        {
            if (!_fadingtoBlack) break;
            timer += Time.deltaTime;
            _curveEval = fadeToBlackCurve.Evaluate(timer);
            _cameraFadeMat.SetFloat(alphaName, _curveEval);
            await Awaitable.EndOfFrameAsync();
        }
        _fadingtoBlack = false;
    }

    public async void CameraFadeIn() //fade from black to scene
    {
        _fadingFromBlack = true;
        if(_fadingtoBlack) _fadingtoBlack = false;
        print("CameraFadeIn");
        _curveEval = fadeFromBlackCurve.Evaluate(0);
        float timer = 0;
        while (timer <= 1)
        {
            if (!_fadingFromBlack) break;
            timer += Time.deltaTime;
            _curveEval = fadeFromBlackCurve.Evaluate(timer);
            _cameraFadeMat.SetFloat(alphaName, _curveEval);
            
            await Awaitable.EndOfFrameAsync();
        }
        _fadingFromBlack = false;
    }
    

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(cameraTransform.position, sphereCheckSize);
    }
}
