using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    private InputSystem_Actions _inputSystem;
    public Vector2 MoveDirection { get; private set; }
    public Vector2 LookDirection{ get; private set; }
    //V1
    public bool JumpPressed{ get; private set; }
    public bool JumpReleased{ get; private set; }
    public bool JumpHeld{ get; private set; }
    
    
    
    public bool FirePressed{ get; private set; }
    public bool InteractPressed{ get; private set; }

    private void Awake() => _inputSystem = new InputSystem_Actions();
    private void OnEnable() => _inputSystem.Enable();
    void OnDisable()=> _inputSystem.Disable();

    private void Update()
    {
        MoveDirection = _inputSystem.Player.Move.ReadValue<Vector2>();
        LookDirection = _inputSystem.Player.Look.ReadValue<Vector2>();
        
        JumpPressed = _inputSystem.Player.Jump.WasPressedThisFrame();
        JumpReleased = _inputSystem.Player.Jump.WasReleasedThisFrame();
        JumpHeld = _inputSystem.Player.Jump.IsPressed();

        FirePressed = _inputSystem.Player.Attack.WasPressedThisFrame();
        InteractPressed = _inputSystem.Player.Interact.WasPressedThisFrame();
    }
}
