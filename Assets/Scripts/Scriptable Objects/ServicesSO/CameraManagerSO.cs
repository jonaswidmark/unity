using System;
using UnityEngine;

[CreateAssetMenu(fileName = "CameraManagerSO", menuName = "ServicesSO/CameraManagerSO")]
public class CameraManagerSO : ScriptableObject
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
    private EventManagerSO eventManager;
    private Vector2 wasdNormalized = Vector2.zero;
    [SerializeField] EventQuaternionArgsSO OnCameraRotationSO;
    [SerializeField] EventVector3ArgsSO OnCameraPositionSO;
    [SerializeField] EventVector3ArgsSO OnCameraLocalPositionSO;
    [SerializeField] Vector3 missionTaskCameraPosition;
    [SerializeField] Vector3 missionTaskCameraLocalPosition;
    [SerializeField] Quaternion missionTaskCameraRotation;
    [SerializeField] float missionTaskMovementSpeed = 1.5f;
    private MissionManagerSO missionManager;
    private GameManagerSO gameManager;
    private Transform cameraRigTransform;

    public void SetMainCamera(Transform mainCamera)
    {
        cameraTransform = mainCamera;
    }
    public void SetCameraRig(Transform cameraRig)
    {
        cameraRigTransform = cameraRig;
    }
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
    private string playerState = "Idle";
    public void StartAction()
    {
        ServiceLocatorSO.InitializeManagers();
        currentState = CameraState.KeyReleased;
        newPosition = cameraRigTransform.position;
        newRotation = cameraRigTransform.rotation;
        newZoom = cameraTransform.localPosition;
        eventManager = ServiceLocatorSO.EventManagerSO;
        eventManager.OnKeyPressed += EventManager_OnKeyPressed;
        eventManager.OnKeyReleased += EventManager_OnKeyReleased;
        eventManager.OnWASDPressed += EventManager_OnWASDPressed;
        eventManager.OnMouseSelect += EventManager_OnMouseSelect;
        eventManager.OnMouseReleased += EventManager_OnMouseReleased;
        eventManager.OnCameraRotation += EventManager_OnCameraRotation;
        eventManager.OnCameraPosition += EventManager_OnCameraPosition;
        eventManager.OnCameraLocalPosition += EventManager_OnCameraLocalPosition;
        missionTaskCameraPosition = cameraRigTransform.position;
        missionTaskCameraRotation = cameraRigTransform.rotation;
        missionManager = ServiceLocatorSO.MissionManagerSO;
        gameManager = ServiceLocatorSO.GameManagerSO;
        gameManager.OnPlayerStatsUpdate += EventManager_OnPlayerStatsUpdate;
    }
    public void SetCameraRigTransform(Transform cameraRigTransform)
    {
        this.cameraRigTransform = cameraRigTransform;
    }
    public void UpdateAction()
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
    private void EventManager_OnPlayerStatsUpdate(object sender, EventArgs e)
    {
        playerState = gameManager.GetCurrentPlayerState();
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
        if(currentState != CameraState.MissionTask)
        {
            HandleMouseCLicked();
            currentState = CameraState.MouseClicked;
        }
    }
    private void EventManager_OnMouseReleased(object sender, EventArgs e)
    {
        if(currentState != CameraState.MissionTask)
        {
            currentState = CameraState.KeyReleased;
        }
    }
    private void EventManager_OnWASDPressed(object sender, Vector2EventArgs e)
    {
        if(currentState == CameraState.MissionTask)
        {
            return;
        }
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
            newPosition = cameraRigTransform.position + dragStartPosition - dragCurrentPosition;
        }
        cameraRigTransform.position = Vector3.Lerp(cameraRigTransform.position, newPosition, movementTime * Time.deltaTime);
    }
    private void HandleMissionTask()
    {
        cameraRigTransform.position = Vector3.Lerp(cameraRigTransform.position, missionTaskCameraPosition, missionTaskMovementSpeed * Time.deltaTime);
        cameraRigTransform.rotation = Quaternion.Lerp(cameraRigTransform.rotation, missionTaskCameraRotation, missionTaskMovementSpeed * Time.deltaTime);
        cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, missionTaskCameraLocalPosition, missionTaskMovementSpeed * Time.deltaTime);
        float threshold = 1.0f;
        if (
        Vector3.Distance(cameraRigTransform.position, missionTaskCameraPosition) < threshold
        && Quaternion.Angle(cameraRigTransform.rotation, missionTaskCameraRotation) < threshold
        && Vector3.Distance(cameraTransform.localPosition, missionTaskCameraLocalPosition) < threshold
        )
        {
            missionManager.EndCurrentMissiontaskCountdown();
            TransitionToIdleState();
        }
    }
    private void HandleMovementInput(string keyPressed)
    {
        if(currentState != CameraState.MissionTask)
        {
            
        
        if(keyPressed =="w" || keyPressed == "upArrow")
        {
            newPosition += (cameraRigTransform.forward * movementSpeed);
        }
        if(keyPressed =="s" || keyPressed == "downArrow")
        {
            newPosition += (cameraRigTransform.forward * -movementSpeed);
        }
        if(keyPressed =="d" || keyPressed == "rightArrow")
        {
            newPosition += (cameraRigTransform.right * movementSpeed);
        }
        if(keyPressed =="a" || keyPressed == "leftArrow")
        {
            newPosition += (cameraRigTransform.right * -movementSpeed);
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
        }
        cameraRigTransform.position = Vector3.Lerp(cameraRigTransform.position, newPosition, movementTime * Time.deltaTime);
        cameraRigTransform.rotation = Quaternion.Lerp(cameraRigTransform.rotation, newRotation, movementTime * Time.deltaTime);
        cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, newZoom, movementTime * Time.deltaTime);
    }
}
