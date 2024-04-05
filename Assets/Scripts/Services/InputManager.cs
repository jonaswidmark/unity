using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel; 
using UnityEngine.UIElements;
public class InputManager : ServiceManager<InputManager>
{
    private PlayerInputActions playerInputActions = null;

    private InputAction mouseSelect = null;
    private InputAction keyboardPressed = null;
    private InputAction wasdPressed = null;
    private Vector3 moveVector = Vector3.zero;
    [SerializeField] EventArgsSO OnMouseSelectSO;
    [SerializeField] EventStringArgsSO OnKeyPressedSO;
    [SerializeField] EventStringArgsSO OnKeyReleasedSO;
    [SerializeField] EventVector2ArgsSO OnWASDPressedSO;
    [SerializeField] EventArgsSO OnMouseReleasedEventArgsSO;
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
        wasdPressed = playerInputActions.Player.WASD;
        wasdPressed.performed += OnWASDPressedAction;
    }
    private void OnDisable()
    {
        playerInputActions.Disable();
        mouseSelect.performed -= OnMouseSelectAction;
        keyboardPressed.performed -= OnKeyPressedAction;
    }
    public void OnWASDPressedAction(InputAction.CallbackContext context)
    {
        Vector2 vector2Arg = playerInputActions.Player.WASD.ReadValue<Vector2>();
        OnWASDPressedSO.RaiseEvent(vector2Arg);
    }
    public void OnKeyPressedAction(InputAction.CallbackContext context)
    {
        /** Any key in the Action map pressed or released **/
        if(context.performed)
        {
            if (context.ReadValue<float>() == 1)
            {
                string controlPath = context.control.path;
                string[] pathParts = controlPath.Split('/');
                string buttonName = pathParts[pathParts.Length - 1];
                OnKeyPressedSO.RaiseEvent(buttonName);
            }
            else 
            {
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
        if (context.ReadValueAsButton())
        {
            OnMouseSelectSO.RaiseEvent(EventArgs.Empty);
        }
        else if (!context.ReadValueAsButton())
        {
            OnMouseReleasedEventArgsSO.RaiseEvent(EventArgs.Empty);
        }
        
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

internal class InputEventPtr
{
    internal bool IsA<T>()
    {
        throw new NotImplementedException();
    }
}