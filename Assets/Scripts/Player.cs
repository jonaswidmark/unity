
using System;
using System.Collections;
using System.Collections.Generic;
//using System.Numerics;
using Unity.VisualScripting;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour, IClickable
{
    private InputManager inputManager;
    private VisualsManager visualsManager;
    private MissionManager missionManager;
    private Rigidbody rb = null;
    public event EventHandler OnIsPlayerWalking;
    public event EventHandler OnIsPlayerIdle;
    private bool isInMovement = false;
    private bool isMovingForward = false;
    private bool isGrounded = false;
    private Vector3 upVector3 = new UnityEngine.Vector3(0,1,0);
    private Vector3 emptyVector3 = new UnityEngine.Vector3(0,0,0);
    private Vector3 forwardVector3 = new UnityEngine.Vector3(0,0,1);
    private Vector3 moveVector = new UnityEngine.Vector3(0,0,0);
    [SerializeField] private LayerMask floor;
    private Transform targetTransform;
    public float duration = 15.0f; // Antal sekunder det tar att nå målet

    private Vector3 initialPosition;
    private float elapsedTime = 0f;
    private bool isMoving = false;
    public float collisionRadius = 1.0f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        
    }
    private void Start()
    {
        inputManager = InputManager.Instance;
        inputManager.OnMouseSelect += InputManager_OnSelect;
        visualsManager = VisualsManager.Instance;
        visualsManager.RemoveVisual(this);
        missionManager = MissionManager.Instance;
        missionManager.OnGoToTransform += MissionManager_OnGoToTransform;
        initialPosition = transform.position;
    }
    private void Update()
    {
        //GroundedSettings();

        
    }
    private void FixedUpdate()
{
    if (!isMoving)
        return;

    Vector3 newInitialPosition = new Vector3(transform.position.x, 0.0f, transform.position.z);
    transform.position = newInitialPosition;

    Vector3 targetDirection = (targetTransform.position - transform.position).normalized;

    float rotationSpeed = 100.0f;
    bool rotationComplete = RotateTowardsDirection(targetDirection, rotationSpeed);
    if (!rotationComplete) return;

    if (IsTouchingTarget())
    {
        Debug.Log("IsTouchingTarget");
        missionManager.EndCurrentMissiontaskCountdown();
        isMoving = false;
        OnIsPlayerIdle?.Invoke(this, EventArgs.Empty);
        return;
    }
    float minSpeed = 0.0f;
    float maxSpeed = 0.05f;
    // Beräkna totala avståndet till målet
    float totalDistance = Vector3.Distance(transform.position, targetTransform.position);
    //Debug.Log(totalDistance/0.05f * Time.deltaTime);
    // Beräkna hur stor del av avståndet som redan har tagits
    float remainingDistance = totalDistance * (1.0f - Mathf.Clamp01(elapsedTime / duration));

    // Beräkna den nya hastigheten baserat på avståndet till målet
    float speed = Mathf.Lerp(minSpeed, maxSpeed, remainingDistance / totalDistance);
    //Debug.Log(speed);
    // Flytta spelaren med den nya hastigheten
    float step = speed * Time.deltaTime;
    transform.position = Vector3.MoveTowards(transform.position, targetTransform.position, 0.05f);

    elapsedTime += Time.deltaTime;
}

    private void FixedUpdateX()
    {
        if (!isMoving)
            return;
        Vector3 newInitialPosition = new Vector3(transform.position.x, 0.0f, transform.position.z);
        transform.position = newInitialPosition;
        Vector3 targetDirection = (targetTransform.position - transform.position).normalized;
        
        float rotationSpeed = 100.0f;
        bool rotationComplete = RotateTowardsDirection(targetDirection, rotationSpeed);
        if (!rotationComplete) return;
        if (IsTouchingTarget())
        {
            isMoving = false;
            OnIsPlayerIdle?.Invoke(this, EventArgs.Empty);
        } 
        elapsedTime += Time.deltaTime;
        
        float t = Mathf.Clamp01(elapsedTime / duration);
        
        Vector3 newTargetPosition = new Vector3(targetTransform.position.x, 0.0f, targetTransform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, newTargetPosition, 0.1f);
        if(transform.position.x-newTargetPosition.x < -0.5f || transform.position.z -newTargetPosition.z < -0.5f)
        {
            //transform.position = Vector3.Lerp(initialPosition, newTargetPosition, t);
            Debug.Log(transform.position-newTargetPosition); 
        }
        
         // Beräkna den nya positionen för spelaren
        //Vector3 newPosition = Vector3.Lerp(initialPosition, newTargetPosition, elapsedTime / duration);
    
        // Flytta spelaren
        //GetComponent<Rigidbody>().MovePosition(newPosition);

       
        
        
    }
    private bool IsTouchingTarget()
    {
        // Utför en sfärisk kollisionskontroll runt spelarens position med angiven radie
        Collider[] colliders = Physics.OverlapSphere(transform.position, collisionRadius);

        // Loopa igenom alla collider som hittades
        foreach (Collider collider in colliders)
        {
            // Kontrollera om någon av kolliderarna tillhör målet
            if (collider.transform == targetTransform)
            {
                // Returnera true om målet hittades
                return true;
            }
        }

        // Returnera false om målet inte hittades
        return false;
    }
    private void MissionManager_OnGoToTransform(object sender, MissionTaskEventArgs e)
    {
        
        initialPosition = transform.position;
        targetTransform = e.missionTask.GetToTransform();
        
        elapsedTime = 0f;
        duration = e.missionTask.TimeToExecute;
        isMoving = true;
        OnIsPlayerWalking?.Invoke(this, EventArgs.Empty);
    }
    private bool RotateTowardsDirection(Vector3 targetDirection, float rotateSpeed)
    {
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);
        
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
        
        // Kontrollera om vi är nära den önskade rotationen
        float angleDifference = Quaternion.Angle(transform.rotation, targetRotation);
        float thresholdAngle = 1.0f; // Ange en tröskelvinkel för att avgöra när rotationen är klar
        //Debug.Log(angleDifference);
        if (angleDifference < thresholdAngle)
        {
            // Rotationen är klar
            return true;
        }
        else
        {
            // Fortsätt rotera
            return false;
        }
    }
    public Transform ObjectTransform
    {
        get
        {
            return transform;
        }
    }
    public GameObject ObjectGameObject
    {
        get
        {
            return gameObject;
        }
    }
    private void InputManager_OnSelect(object sender, EventArgs e)
    {
        if(WasSelected())
        {
            visualsManager.SetVisual(this);
        }
        else 
        {
            visualsManager.RemoveVisual(this);
        }
    }
    public bool WasSelected()
    {
        return Utils.WasSelected(this);
    }

    
    /* 
    private void InputManager_OnMouseX(object sender, EventArgs e)
    {
        Vector2 mouseXDelta = inputManager.GetMouseXDelta();
        float sensitivity = 0.6f;
        transform.Rotate(Vector3.up,mouseXDelta.x * sensitivity);
        //transform.Rotate(Vector3.left, mouseXDelta.y);
    } */
    private void GroundedSettings()
    {
       /*  RaycastHit hit;
        float raycastDistance = 3f;
        float groundedApproximate = 0.1f;
        isGrounded = false;
        if(Physics.Raycast(transform.position, Vector3.down, out hit, raycastDistance, floor) 
        || transform.position == new Vector3(transform.position.x,0,transform.position.z))
        {
            if(hit.distance <= groundedApproximate)
            {
                isGrounded = true;
            }
        } */
    }
    
    public bool GetIsGrounded()
    {
        return isGrounded;
    }
    public bool GetIsInMovement()
    {
        return isInMovement;
    }
    public bool GetIsMovingForward()
    {
        return isMovingForward;
    }
    
}
