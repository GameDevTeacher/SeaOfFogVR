using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BoatMovement : MonoBehaviour
{
    private Rigidbody _rb;
    private List<OarScript> _oarScripts;
    [SerializeField] private List<Vector3> _oarVectors;

    private void Awake()
    {
        _oarScripts = GetComponentsInChildren<OarScript>().ToList();
        
        _rb = gameObject.GetComponent<Rigidbody>();
    }
    
    private Vector3 _moveVector()
    {
        Vector3 V3 = Vector3.zero;
        foreach (var vector in _oarVectors)
        {
            V3 += vector;
        }
        return V3;
    }

    private void Update()
    {
        
        _rb.AddForce(_moveVector() * Time.deltaTime, ForceMode.VelocityChange);
        Debug.Log(_rb.linearVelocity.magnitude);
        _oarVectors.Clear();
        foreach (var oar in _oarScripts)
        {
            _oarVectors.Add(oar.OarVector);
        }
    }
}
