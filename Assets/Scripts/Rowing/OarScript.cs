using System;
using UnityEngine;

public class OarScript : MonoBehaviour
{
    [SerializeField] private Transform _handleTransform;
    private Vector3 _targetOrientation
    {
        get { return (_handleTransform.position - transform.position); }
    }

    private float _maxDistance;
    private OarTip _oarTip;
    public Vector3 OarVector = Vector3.zero;

    

    
    
    
    private void Start()
    {
        _maxDistance = _targetOrientation.magnitude;
        _oarTip = GetComponentInChildren<OarTip>();
        FmodController.current.RowingAmbience();
    }
    
    
    private void Update()
    {
        gameObject.transform.rotation = Quaternion.LookRotation(_targetOrientation, Vector3.up);
        Vector3 c = _targetOrientation;
        c.Normalize();
        c *= _maxDistance;
        
        _handleTransform.position = transform.position + c;

        if (_oarTip._touchingWater)
        {
            var OarTip = gameObject.GetComponentInChildren<OarTip>();
            OarVector = OarTip.RowingVector;
        } else if (!_oarTip._touchingWater)
        {
            OarVector = Vector3.zero;
        }
        
    }

}
