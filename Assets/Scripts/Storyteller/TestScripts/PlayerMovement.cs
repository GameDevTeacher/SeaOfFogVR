using UnityEngine;
[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    #region VARIABLES
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _jumpSpeed = 3f;
    [SerializeField] private float _gravity = -9.8f;

    [SerializeField] private float _moveSmoothtime = 0.3f;
    //da fack does this do??
    private float _velocityY;
    
    private Vector2 _currentVelocity = Vector2.zero;
    private Vector2 _currentDirection = Vector2.zero;
    private CharacterController _characterController;
    
    #endregion

    private void Start() => _characterController = GetComponent<CharacterController>();

    public void UpdateMovement(Vector2 targetDirection, bool jumpPressed)
    {
        if (_characterController.isGrounded) { _velocityY = -2f; }

        _currentDirection = Vector2.SmoothDamp(
            _currentDirection, targetDirection,
            ref _currentVelocity, _moveSmoothtime);
        if (_characterController.isGrounded && jumpPressed)
        {
            _velocityY = Mathf.Sqrt(_jumpSpeed * -2f * _gravity);
        }
        
        _velocityY += _gravity * Time.deltaTime;
        var velocity = (transform.forward * _currentDirection.y 
                        + transform.right * _currentDirection.x) 
                        * _moveSpeed + Vector3.up * _velocityY;
        
        _characterController.Move(velocity * Time.deltaTime);
    }
}
