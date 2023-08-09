using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControlsInitializer : MonoBehaviour
{
    // Данный скрипт инициализирует все настройки управления игрока
    [SerializeField] private PauseMenu _pauseMenu;
    
    private PlayerInputActions _input;
    private Action<InputAction.CallbackContext> _inputMovement;
    public Vector2 MoveInput { get; private set; }
    public Vector2 CameraMoveInput { get; private set; }
    public bool JumpInput { get; private set; }
    public bool ShootInput { get; private set; }
    public bool ReloadInput { get; private set; }
    public event Action Pause;

    private void SetMovementInput(InputAction.CallbackContext ctx) => 
        MoveInput = ctx.ReadValue<Vector2>();
    private void SetCameraMoveInput(InputAction.CallbackContext ctx) =>
        CameraMoveInput = ctx.ReadValue<Vector2>();
    private void SetJumpInput(InputAction.CallbackContext ctx) =>
        JumpInput = ctx.ReadValueAsButton();
    private void SetShootInput(InputAction.CallbackContext ctx) =>
        ShootInput = ctx.ReadValueAsButton();
    private void SetReloadInput(InputAction.CallbackContext ctx) =>
        ReloadInput = ctx.ReadValueAsButton();
    private void SetPause(InputAction.CallbackContext ctx)
    {
        Pause?.Invoke();
    }


    private void Awake()
    {
        _input = new PlayerInputActions();
    }
    
    private void OnEnable()
    {
        _input.Enable();
        _input.Player.Move.performed += SetMovementInput;
        _input.Player.Move.canceled += SetMovementInput;
        _input.Player.Jump.performed += SetJumpInput;
        _input.Player.Jump.canceled += SetJumpInput;
        _input.Player.CameraMove.performed += SetCameraMoveInput;
        _input.Player.CameraMove.canceled += SetCameraMoveInput;
        _input.Player.Shoot.performed += SetShootInput;
        _input.Player.Shoot.canceled += SetShootInput;
        _input.Player.Reload.performed += SetReloadInput;
        _input.Player.Reload.canceled += SetReloadInput;
        _input.Player.Pause.started += SetPause;
    }

    private void OnDisable()
    {
        _input.Disable();
        _input.Player.Move.performed -= SetMovementInput;
        _input.Player.Move.canceled -= SetMovementInput;
        _input.Player.Jump.performed -= SetJumpInput;
        _input.Player.Jump.canceled -= SetJumpInput;
        _input.Player.CameraMove.performed -= SetCameraMoveInput;
        _input.Player.CameraMove.canceled -= SetCameraMoveInput;
        _input.Player.Shoot.performed -= SetShootInput;
        _input.Player.Shoot.canceled -= SetShootInput;
        _input.Player.Reload.performed -= SetReloadInput;
        _input.Player.Reload.canceled -= SetReloadInput;
        _input.Player.Pause.started -= SetPause;
    }
}
