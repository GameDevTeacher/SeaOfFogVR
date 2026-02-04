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

    
    private MeshRenderer _meshRenderer;
    [SerializeField] private Material _waterMaterial;
    [SerializeField] private Material _dryMaterial;
    
    
    
    private void Start()
    {
        _maxDistance = _targetOrientation.magnitude;
        _meshRenderer = GetComponentInChildren<MeshRenderer>();
        _oarTip = GetComponentInChildren<OarTip>();
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
            _meshRenderer.material = _waterMaterial;
            var OarTip = gameObject.GetComponentInChildren<OarTip>();
            OarVector = OarTip.RowingVector;
        } else if (!_oarTip._touchingWater)
        {
            _meshRenderer.material = _dryMaterial;
            OarVector = Vector3.zero;
        }
    }

}
