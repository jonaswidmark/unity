using System;
using UnityEngine;
using UnityEngine.InputSystem;
public class InputManager : ServiceManager<InputManager>
{
    private PlayerInputActions playerInputActions = null;
    private InputAction mouseSelect = null;
    private InputAction keyboardPressed = null;
    private Vector3 moveVector = Vector3.zero;
    [SerializeField] EventArgsSO OnMouseSelectSO;
    [SerializeField] EventStringArgsSO OnKeyPressedSO;
    [SerializeField] EventStringArgsSO OnKeyReleasedSO;
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
        keyboardPressed = playerInputActions.Player.Keyboard;
        keyboardPressed.performed += OnKeyPressedAction;
    }
    private void OnDisable()
    {
        playerInputActions.Disable();
        mouseSelect.performed -= OnMouseSelectAction;
        keyboardPressed.performed -= OnKeyPressedAction;
    }
    public void OnKeyPressedAction(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            if (context.ReadValue<float>() == 1)
            {
                //string keyPressed = context.action.name;
                string controlPath = context.control.path;
                string[] pathParts = controlPath.Split('/');
                string buttonName = pathParts[pathParts.Length - 1];
                OnKeyPressedSO.RaiseEvent(buttonName);
            }
            else 
            {
                //string keyPressed = context.action.name;
                string controlPath = context.control.path;
                string[] pathParts = controlPath.Split('/');
                string buttonName = pathParts[pathParts.Length - 1];
                OnKeyReleasedSO.RaiseEvent(buttonName);
            }
        }
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
