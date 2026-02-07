using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerInput : MonoBehaviour
    {
        private InputSystem_Actions _inputSystem;

        public Vector2 MoveDirection { get; private set; }
        public Vector2 LookDirection { get; private set; }
    
        public bool JumpPressed { get; private set; }
        public bool JumpReleased { get; private set; }
        public bool JumpHeld { get; private set; }
    
        public bool FirePressed { get; private set; }
        public bool InteractPressed { get; private set; }

        #region ALTERNATIVE
        // Version 2 of calling values
        public InputAction JumpAction { get; private set; }
        public InputAction FireAction { get; private set; }
        public InputAction InteractAction { get; private set; }
        #endregion

        private void Awake() => _inputSystem = new InputSystem_Actions();
        private void OnEnable() => _inputSystem.Enable();
        private void OnDisable() => _inputSystem.Disable();

        private void Update()
        {
            MoveDirection = _inputSystem.Player.Move.ReadValue<Vector2>();
            LookDirection = _inputSystem.Player.Look.ReadValue<Vector2>();

            JumpPressed = _inputSystem.Player.Jump.WasPressedThisFrame();
            JumpReleased = _inputSystem.Player.Jump.WasReleasedThisFrame();
            JumpHeld = _inputSystem.Player.Jump.IsPressed();

            FirePressed = _inputSystem.Player.Attack.WasPressedThisFrame();
            InteractPressed = _inputSystem.Player.Interact.WasPressedThisFrame();

            #region ALTERNATIVE
            JumpAction = _inputSystem.Player.Jump;
            FireAction = _inputSystem.Player.Attack;
            InteractAction = _inputSystem.Player.Interact;
            #endregion
        }
    }
}