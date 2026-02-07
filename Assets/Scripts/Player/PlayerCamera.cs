using UnityEngine;

namespace Player
{
    public class PlayerCamera : MonoBehaviour
    {
        [SerializeField] private Transform _playerCamera;
        [SerializeField] private float _cursorSensitivity = 3.5f;
        [SerializeField] private float _cursorSmoothTime = 0.03f;
        [SerializeField] private bool _lockCursor = true;

        private Vector2 _currentCursorDelta = Vector2.zero;
        private Vector2 _currentCursorsDeltaVelocity = Vector2.zero;

        private float _cameraPitch;

        private void Start()
        {
            if (!_lockCursor) return;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        public void UpdateLookDirection(Vector2 direction)
        {
            _currentCursorDelta = Vector2.SmoothDamp(
                _currentCursorDelta, // Current Position (relative to last frame)
                direction, // target position
                ref _currentCursorsDeltaVelocity,
                _cursorSmoothTime // Time to reach target
            );

            _cameraPitch -= _currentCursorDelta.y * _cursorSensitivity;
            _cameraPitch = Mathf.Clamp(_cameraPitch, -70f, 70f);
            _playerCamera.localEulerAngles = Vector3.right * _cameraPitch;

            transform.Rotate(Vector3.up * (_currentCursorDelta.x * _cursorSensitivity));
        }
    }
}