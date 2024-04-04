using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class CameraManager : ServiceManager<CameraManager>
{
    [SerializeField] float movementSpeed;
    [SerializeField] float movementTime;
    [SerializeField] Vector3 newPosition;
    private EventManager eventManager;
    private enum CameraState
    {
        KeyPressed,
        KeyReleased
    }
    private CameraState currentState;
    private string keyPressedOrReleased;
    private void Start()
    {
        currentState = CameraState.KeyReleased;
        newPosition = transform.position;
        eventManager = ServiceLocator.EventManager;
        eventManager.OnKeyPressed += EventManager_OnKeyPressed;
        eventManager.OnKeyReleased += EventManager_OnKeyReleased;
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
    private void EventManager_OnKeyPressed(object sender, StringEventArgs e)
    {
        keyPressedOrReleased = e.StringArg;
        TransitionToKeyPressedState();
    }
    private void EventManager_OnKeyReleased(object sender, StringEventArgs e)
    {
        keyPressedOrReleased = e.StringArg;
        TransitionToKeyReleasedState();
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
        transform.position = Vector3.Lerp(transform.position, newPosition, movementTime * Time.deltaTime);
    }
}
