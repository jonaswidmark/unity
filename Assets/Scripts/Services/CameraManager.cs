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
    [SerializeField] EventQuaternionArgsSO OnCameraRotationSO;
    [SerializeField] EventVector3ArgsSO OnCameraPositionSO;
    [SerializeField] EventVector3ArgsSO OnCameraLocalPositionSO;
    [SerializeField] Vector3 missionTaskCameraPosition;
    [SerializeField] Vector3 missionTaskCameraLocalPosition;
    [SerializeField] Quaternion missionTaskCameraRotation;
    [SerializeField] float missionTaskMovementSpeed = 1.5f;

    private enum CameraState
    {
        Idle,
        MissionTask,
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
        eventManager.OnCameraRotation += EventManager_OnCameraRotation;
        eventManager.OnCameraPosition += EventManager_OnCameraPosition;
        eventManager.OnCameraLocalPosition += EventManager_OnCameraLocalPosition;
        missionTaskCameraPosition = transform.position;
        //missionTaskCameraRotation = transform.rotation;

    }
    private void Update()
    {
        switch (currentState)
        {
            case CameraState.Idle:
                break;
            case CameraState.MissionTask:
                HandleMissionTask();
                break;
            case CameraState.KeyPressed:
                HandleMovementInput(keyPressedOrReleased);
                break;
            case CameraState.KeyReleased:
                TransitionToIdleState();
                break;
            case CameraState.MouseClicked:
                HandleMouseDown();
                break;
        }
    }
    public void TransitionToIdleState()
    {
        currentState = CameraState.Idle;
    }
    public void TransitionToMissionTaskState()
    {
        currentState = CameraState.MissionTask;
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
    private void EventManager_OnCameraRotation(object sender, QuaternionEventArgs e)
    {
        missionTaskCameraRotation = e.QuaternionArg;
        TransitionToMissionTaskState();
    }
    private void EventManager_OnCameraPosition(object sender, Vector3EventArgs e)
    {
        missionTaskCameraPosition = e.Vector3Arg;
        TransitionToMissionTaskState();
    }
    private void EventManager_OnCameraLocalPosition(object sender, Vector3EventArgs e)
    {
        missionTaskCameraLocalPosition = e.Vector3Arg;
        TransitionToMissionTaskState();
    }
    private void EventManager_OnMouseSelect(object sender, EventArgs e)
    {
        HandleMouseCLicked();
        currentState = CameraState.MouseClicked;
    }
    private void EventManager_OnMouseReleased(object sender, EventArgs e)
    {
        currentState = CameraState.KeyReleased;
    }
    private void EventManager_OnWASDPressed(object sender, Vector2EventArgs e)
    {
        /* if(currentState == CameraState.MissionTask)
        {
            return;
        } */
        wasdNormalized = e.Vector2Arg.normalized;
        // TODO: apply normalized vector on movement in HandleMovementInput
    }
    private void EventManager_OnKeyPressed(object sender, StringEventArgs e)
    { 
        /* if(currentState == CameraState.MissionTask)
        {
            return;
        } */
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
        /* if(currentState == CameraState.MissionTask)
        {
            return;
        } */
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
        /* if(currentState == CameraState.MissionTask)
        {
            return;
        } */
        Plane plane = new Plane(Vector3.up, Vector3.zero);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float entry;
        if(plane.Raycast(ray, out entry))
        {
            dragStartPosition = ray.GetPoint(entry);
        }
    }
    private void HandleMouseDown()
    {
        /* if(currentState == CameraState.MissionTask)
        {
            return;
        } */
        Plane plane = new Plane(Vector3.up, Vector3.zero);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float entry;
        if(plane.Raycast(ray, out entry))
        {
            dragCurrentPosition = ray.GetPoint(entry);
            newPosition = transform.position + dragStartPosition - dragCurrentPosition;
        }
        transform.position = Vector3.Lerp(transform.position, newPosition, movementTime * Time.deltaTime);
    }
    private void HandleMissionTask()
    {
        transform.position = Vector3.Lerp(transform.position, missionTaskCameraPosition, missionTaskMovementSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, missionTaskCameraRotation, missionTaskMovementSpeed * Time.deltaTime);
        cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, missionTaskCameraLocalPosition, missionTaskMovementSpeed * Time.deltaTime);
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
        Debug.Log(cameraTransform.localPosition);
    }
}
