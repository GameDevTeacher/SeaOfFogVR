using System;
using UnityEngine;

public class BoatLeavingFishmanIsle : MonoBehaviour
{
    public Rigidbody _rigidbody;
    private Vector3 _moveDirection;
    public float moveSpeed = 2;
    [SerializeField] private GameObject _islePosition;

    
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        //StoryEventsController.current.onBoatArrivingFishmanIsle += UpdateBoatMoving;
    }

    
    public void UpdateBoatMoving()
    {
        _moveDirection = transform.position - _islePosition.transform.position;
        _rigidbody.linearVelocity = _moveDirection.normalized * moveSpeed;
    }
}
