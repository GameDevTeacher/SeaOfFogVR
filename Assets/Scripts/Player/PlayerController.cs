using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(PlayerAttack))]
    [RequireComponent(typeof(PlayerInput), typeof(PlayerMovement), typeof(PlayerCamera))]
    public class PlayerController : MonoBehaviour
    {
        // SOLID
        private PlayerInput _input;
        private PlayerMovement _movement;
        private PlayerCamera _camera;
        private PlayerAttack _attack;
    
        private void Start()
        {
            _input = GetComponent<PlayerInput>();
            _movement = GetComponent<PlayerMovement>();
            _camera = GetComponent<PlayerCamera>();
            _attack = GetComponent<PlayerAttack>();
        }

        private void Update()
        {
            _movement.UpdateMovement(_input.MoveDirection, _input.JumpPressed);
            _camera.UpdateLookDirection(_input.LookDirection);
            _attack.UpdateShooting(_input.FireAction.WasPressedThisFrame());
        }
    }
}