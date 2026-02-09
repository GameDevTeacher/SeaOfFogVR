using System;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private Transform _playerCamera;
    [SerializeField] private float _cursorSensitivity = 3.5f;
    [SerializeField] [Range(0.0f, 0.5f)] private float cursorSmoothTime = 0.03f;
    [SerializeField] private bool lockCursor = true;
    
    private Vector2 _currentCursorDelta = Vector2.zero;
    private Vector2 _currentCursorDeltaVelocity = Vector2.zero;

    private float _cameraPitch;

    private void Start()
    {
        if (!lockCursor) return;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void UpdateLookDirection(Vector2 lookDirection)
    {
        // dampens the jitter as it moves from the current vector of  _currentCursorDelta, moving to "Target" 
        //vector position towards lookdirection. 
        //smoothTime 	Approximately the time it will take to reach the target.
        //A smaller value will reach the target faster. here cursorSmoothTime.
        _currentCursorDelta = Vector2.SmoothDamp(_currentCursorDelta, lookDirection,
            ref _currentCursorDeltaVelocity, cursorSmoothTime);
        
        //Camera pitch rotating on the x axis
        _cameraPitch -= _currentCursorDelta.y * _cursorSensitivity;
        
        //how far we (in degrees) we can look down or up)
        _cameraPitch = Mathf.Clamp(_cameraPitch, -80, 80);

        _playerCamera.localEulerAngles = Vector3.right * _cameraPitch;
        
        transform.Rotate(Vector3.up * (_currentCursorDelta.x * _cursorSensitivity));
    }
}
