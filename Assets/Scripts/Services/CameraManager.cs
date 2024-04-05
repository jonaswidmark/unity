using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class CameraManager : ServiceManager<CameraManager>
{
    [SerializeField] float normalSpeed;
    [SerializeField] float fastSpeed;
    [SerializeField] float movementSpeed;
    [SerializeField] float movementTime;
    [SerializeField] Vector3 newPosition;
    [SerializeField] Quaternion newRotation;
    [SerializeField] float rotationAmount;
    [SerializeField] Transform cameraTransform;
    [SerializeField] Vector3 zoomAmount;
    [SerializeField] Vector3 newZoom;
    [SerializeField] Vector3 dragStartPosition;
    [SerializeField] Vector3 dragCurrentPosition;
    private EventManager eventManager;
    private Vector2 wasdNormalized = Vector2.zero;
    private enum CameraState
    {
        KeyPressed,
        KeyReleased,
        MouseClicked
    }
    private CameraState currentState;
    private string keyPressedOrReleased;
    private void Start()
    {
        currentState = CameraState.KeyReleased;
        newPosition = transform.position;
        newRotation = transform.rotation;
        newZoom = cameraTransform.localPosition;
        eventManager = ServiceLocator.EventManager;
        eventManager.OnKeyPressed += EventManager_OnKeyPressed;
        eventManager.OnKeyReleased += EventManager_OnKeyReleased;
        eventManager.OnWASDPressed += EventManager_OnWASDPressed;
        eventManager.OnMouseSelect += EventManager_OnMouseSelect;
        eventManager.OnMouseReleased += EventManager_OnMouseReleased;
    }
    private void Update()
    {
        switch (currentState)
        {
            case CameraState.KeyPressed:
                HandleMovementInput(keyPressedOrReleased);
                break;
            case CameraState.KeyReleased:
                break;
            case CameraState.MouseClicked:
                HandleMouseCLicked();
                break;
        }
    }
    
    public void TransitionToKeyPressedState()
    {
        currentState = CameraState.KeyPressed;
    }
    public void TransitionToKeyReleasedState()
    {
        currentState = CameraState.KeyReleased;
    }
    public void TransitionToMouseClickedState()
    {
        currentState = CameraState.MouseClicked;
    }
    private void EventManager_OnMouseSelect(object sender, EventArgs e)
    {
        currentState = CameraState.MouseClicked;
    }
    private void EventManager_OnMouseReleased(object sender, EventArgs e)
    {
        currentState = CameraState.KeyReleased;
    }
    private void EventManager_OnWASDPressed(object sender, Vector2EventArgs e)
    {
        wasdNormalized = e.Vector2Arg.normalized;
        // TODO: apply normalized vector on movement in HandleMovementInput
    }
    private void EventManager_OnKeyPressed(object sender, StringEventArgs e)
    { 
        if(e.StringArg == "shift")
        {
            movementSpeed = fastSpeed;
        }
        else
        {
            keyPressedOrReleased = e.StringArg;
        }
        TransitionToKeyPressedState();
    }
    private void EventManager_OnKeyReleased(object sender, StringEventArgs e)
    {
        if(e.StringArg == "shift")
        {
            movementSpeed = normalSpeed;
        }
        else
        {
            keyPressedOrReleased = e.StringArg;
        }
        TransitionToKeyReleasedState();
    }
    private void HandleMouseCLicked()
    {
        Debug.Log("Handle mouse clicked");
    }
    private void HandleMovementInput(string keyPressed)
    {
        if(keyPressed =="w" || keyPressed == "upArrow")
        {
            newPosition += (transform.forward * movementSpeed);
        }
        if(keyPressed =="s" || keyPressed == "downArrow")
        {
            newPosition += (transform.forward * -movementSpeed);
        }
        if(keyPressed =="d" || keyPressed == "rightArrow")
        {
            newPosition += (transform.right * movementSpeed);
        }
        if(keyPressed =="a" || keyPressed == "leftArrow")
        {
            newPosition += (transform.right * -movementSpeed);
        }
        if(keyPressed == "q")
        {
            newRotation *= Quaternion.Euler(Vector3.up * rotationAmount);
        }
        if(keyPressed == "e")
        {
            newRotation *= Quaternion.Euler(Vector3.up * -rotationAmount);
        }
        if(keyPressed == "r")
        {
            float minY = 2f;
            if(newZoom.y > minY)
            {
                newZoom += zoomAmount;
            }
        }
        if(keyPressed == "f")
        {
            float maxY = 161.2f;
            if(newZoom.y < maxY)
            {
                newZoom -= zoomAmount;
            }
        }
        transform.position = Vector3.Lerp(transform.position, newPosition, movementTime * Time.deltaTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, movementTime * Time.deltaTime);
        cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, newZoom, movementTime * Time.deltaTime);
    }
}
