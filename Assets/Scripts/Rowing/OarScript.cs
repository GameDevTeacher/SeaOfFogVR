using UnityEngine;

public class OarScript : MonoBehaviour
{
    [SerializeField] private Transform _handleTransform;
    private Vector3 _targetOrientation
    {
        get { return (_handleTransform.position - transform.position); }
    }

    private float _maxDistance;

    private void Start()
    {
        _maxDistance = _targetOrientation.magnitude;
    }
    
    
    private void Update()
    {
        gameObject.transform.rotation = Quaternion.LookRotation(_targetOrientation, Vector3.up);
        Vector3 c = _targetOrientation;
        c.Normalize();
        c *= _maxDistance;
        
        _handleTransform.position = transform.position + c;
    }
}
