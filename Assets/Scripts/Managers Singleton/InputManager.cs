using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance {get; private set;}
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
        if(mouseXDelta.magnitude > 0f)
        {
            OnMouseX?.Invoke(this, EventArgs.Empty);
        }
    }
    /* public void OnMovementPerformed(InputAction.CallbackContext value)
    {
        moveVector = value.ReadValue<Vector3>();
        if(moveVector.magnitude > 0f)
        {
            if(moveVector == new Vector3(-1,0,0))
            {
                isWalking = false;
                OnIsMovingLeft?.Invoke(this, EventArgs.Empty);   
            }
            else if(moveVector == new Vector3(1,0,0))
            {
                isWalking = false;
                OnIsMovingRight?.Invoke(this, EventArgs.Empty);   
            }
            else if(moveVector == new Vector3(0,1,0))
            {
                OnIsJumping?.Invoke(this, EventArgs.Empty);   
            }
            else if(moveVector == new Vector3(0,0,1))
            {
                isWalking = true;
                OnIsMovingForward?.Invoke(this, EventArgs.Empty);
            }
            else if(moveVector == new Vector3(0,0,-1))
            {
                isWalking = true;
                OnIsMovingBackwards?.Invoke(this, EventArgs.Empty);
            }
            
        }
        else if (moveVector.magnitude == 0)
        {
            isWalking = false;
            OnNotMoving?.Invoke(this, EventArgs.Empty);
        }
    } */
   /*  public void OnMovementCanceled(InputAction.CallbackContext value)
    {
        Debug.Log("Cancelled!");
        //moveVector = Vector3.zero;
    } */
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
