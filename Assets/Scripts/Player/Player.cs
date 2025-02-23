using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private PlayerInput _playerInput;
    private Mover _mover;    
    private Aim _aim;
    private Shooter _shooter;
    private float _inputDirection;

    private void Awake()
    {
        _playerInput = new PlayerInput();
        _mover = GetComponent<Mover>();
        _aim = GetComponent<Aim>();
        _shooter = GetComponent<Shooter>();
    }

    private void Update()
    {
        _inputDirection = _playerInput.Player.Move.ReadValue<float>();
        _mover.Move(_inputDirection);

        _aim.SetMousePosition(_playerInput.Mouse.Position.ReadValue<Vector2>());
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        _mover.Jump();
    }

    private void OnShoot(InputAction.CallbackContext context)
    {
        _shooter.Shoot();
        AudioManager.Instance.PlayPlayerShot();
    }

    private void OnEnable()
    {
        _playerInput.Enable();

        _playerInput.Player.Jump.performed += OnJump;
        _playerInput.Mouse.Shoot.performed += OnShoot;
    }

    private void OnDisable()
    {
        _playerInput.Player.Jump.performed -= OnJump;
        _playerInput.Mouse.Shoot.performed -= OnShoot;

        _playerInput.Disable();
    }
}
