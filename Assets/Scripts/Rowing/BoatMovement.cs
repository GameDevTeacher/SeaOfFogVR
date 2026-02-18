using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BoatMovement : MonoBehaviour
{
    private Rigidbody _rb;
    [SerializeField] private List<OarScript> _oarScripts;
    [SerializeField] private List<Vector3> _oarVectors;

    [SerializeField] private List<Transform> _oarAnchors;

    [SerializeField] private float _sensitivity = 5;

    private void Awake()
    {
        _rb = gameObject.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        _oarVectors.Clear();
        foreach (var oar in _oarScripts)
        {
            _oarVectors.Add(oar.OarVector);
        }
        
        _rb.AddForceAtPosition(_oarVectors[0]*_sensitivity * _sensitivity, _oarAnchors[0].position, ForceMode.Force);
        _rb.AddForceAtPosition(_oarVectors[1]*_sensitivity * _sensitivity, _oarAnchors[1].position, ForceMode.Force);
    }
}
