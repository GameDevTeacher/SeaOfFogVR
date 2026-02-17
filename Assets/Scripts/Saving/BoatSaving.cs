using System;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class BoatSaving : MonoBehaviour
{
    private Transform _position;
    private Collider _handleRightCollider;
    private Collider _handleLeftCollider;
    private Collider _boatCollider;
    private Rigidbody _boatRigidbody;
    
    private void Awake()
    {
        _position = gameObject.transform;   
        _handleLeftCollider = gameObject.transform.GetChild(2).gameObject.GetComponent<Collider>();
        _handleRightCollider = gameObject.transform.GetChild(1).gameObject.GetComponent<Collider>();
        _boatCollider = gameObject.transform.GetChild(0).gameObject.GetComponent<Collider>();
        _boatRigidbody = gameObject.transform.GetComponent<Rigidbody>();
        _boatRigidbody.interpolation = RigidbodyInterpolation.None;
        
    }

    private async void Start()
    {
        try
        {
            await Awaitable.FixedUpdateAsync();
            _boatRigidbody.interpolation = RigidbodyInterpolation.Interpolate;
        }
        catch (Exception e)
        {
            print(e.Message);
            throw; // TODO handle exception
        }
    }
    
    public void Save(ref BoatSaveData data)
    {
        data.position = _position.position;
        data.rotation = _position.rotation;
        data.handleColliderLeft = _handleLeftCollider.enabled;
        data.handleColliderRight = _handleRightCollider.enabled;
        data.boatCollider = _boatCollider.enabled;
    }

    public void Load(BoatSaveData data)
    {
        transform.position = data.position;
        transform.rotation = data.rotation;
        _handleLeftCollider.enabled = data.handleColliderLeft;
        _handleRightCollider.enabled = data.handleColliderRight;
        _boatCollider.enabled = data.boatCollider;
        
    }
    
}

[System.Serializable]
public struct BoatSaveData
{
    public Vector3 position;
    public quaternion rotation;
    public bool boatCollider;
    public bool handleColliderLeft;
    public bool handleColliderRight;
    
}
