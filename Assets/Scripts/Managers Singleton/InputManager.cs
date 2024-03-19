using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }
    private PlayerInputActions playerInputActions = null;
    private InputAction mouseSelect = null;
    private Vector3 moveVector = Vector3.zero;

    public event EventHandler OnMouseX;
    public event EventHandler OnMouseSelect;
    private Vector2 mouseXDelta;

    public void Awake()
    {
        if (Instance != null)
        {
            Debug.Log("There's more than one InputManager! " + transform + " - " + Instance);
            Destroy(gameObject);
            return;
        }
        Instance = this;
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
        OnMouseSelect?.Invoke(this, EventArgs.Empty);
    }

    public void OnMouseXAction(InputAction.CallbackContext context)
    {
        Debug.Log("OnMouseXAction");
        mouseXDelta = context.ReadValue<Vector2>();
        if (mouseXDelta.magnitude > 0f)
        {
            OnMouseX?.Invoke(this, EventArgs.Empty);
        }
    }
    
    public Vector3 GetMoveVector()
    {
        return moveVector;
    }
    /* public bool GetIsWalking()
    {
        return isWalking;
    }  */
    public Vector2 GetMouseXDelta()
    {
        return mouseXDelta;
    }

}
