using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : ServiceManager<InputManager>
{
    private PlayerInputActions playerInputActions = null;
    private InputAction mouseSelect = null;
    private Vector3 moveVector = Vector3.zero;
    [SerializeField] EventArgsSO OnMouseSelectSO;
    private Vector2 mouseXDelta;

    public void Awake()
    {
        playerInputActions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        playerInputActions.Enable();

        mouseSelect = playerInputActions.Player.MouseSelect;
        mouseSelect.performed += OnMouseSelectAction;
    }
    private void OnDisable()
    {
        playerInputActions.Disable();
        mouseSelect.performed -= OnMouseSelectAction;
    }
    public Vector2 GetMouseScreenPosition()
    {
        return Mouse.current.position.ReadValue();
    }
    public void OnMouseSelectAction(InputAction.CallbackContext context)
    {
        OnMouseSelectSO.RaiseEvent(EventArgs.Empty);
    }
    public Vector3 GetMoveVector()
    {
        return moveVector;
    }
    public Vector2 GetMouseXDelta()
    {
        return mouseXDelta;
    }
}
