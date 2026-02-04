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

    private bool _touchingWater;

    
    private MeshRenderer _meshRenderer;
    [SerializeField] private Material _waterMaterial;
    [SerializeField] private Material _dryMaterial;
    
    
    
    private void Start()
    {
        _maxDistance = _targetOrientation.magnitude;
        _meshRenderer = GetComponentInChildren<MeshRenderer>();
    }
    
    
    private void Update()
    {
        gameObject.transform.rotation = Quaternion.LookRotation(_targetOrientation, Vector3.up);
        Vector3 c = _targetOrientation;
        c.Normalize();
        c *= _maxDistance;
        
        _handleTransform.position = transform.position + c;

        if (_touchingWater)
        {
            _meshRenderer.material = _waterMaterial;
        } else if (!_touchingWater)
        {
            _meshRenderer.material = _dryMaterial;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Water"))
        {
            _touchingWater = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Water"))
        {
            _touchingWater = false;
        }
    }
}
