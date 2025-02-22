using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private PlayerMover _playerMover;
    private PlayerInput _playerInput;
    private float _inputDirection;

    private void Awake()
    {
        _playerInput = new PlayerInput();
        _playerMover = GetComponent<PlayerMover>();
    }

    private void Start()
    {
    }

    private void Update()
    {
        _inputDirection = _playerInput.Player.Move.ReadValue<float>();
        _playerMover.Move(_inputDirection);
    }

    private void OnEnable()
    {
        _playerInput.Enable();
    }

    private void OnDisable()
    {
        _playerInput.Disable();
    }
}
