
using System;
using System.Collections;
using System.Collections.Generic;
//using System.Numerics;
using Unity.VisualScripting;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Player : BaseSceneObject, IClickable
{
    /* private EventManager eventManager;
    private VisualsManager visualsManager;
    private MissionManager missionManager; */
    private Rigidbody rb = null;
    public event EventHandler OnIsPlayerWalking;
    public event EventHandler OnIsPlayerIdle;
    [SerializeField] private Animator animator;
    private bool isInMovement = false;
    private bool isMovingForward = false;
    private bool isGrounded = false;

    [SerializeField] private LayerMask floor;
    private Transform targetTransform;
    public float duration = 15.0f; // Antal sekunder det tar att nå målet

    private Vector3 initialPosition;
    private float elapsedTime = 0f;
    private bool isMoving = false;
    public float collisionRadius = 2.0f;
    private float initialDistanceToTarget;
    private Collider targetCollider;
    private Collider playerCollider;
    private Vector3 closestTargetPoint;
    private Vector3 targetColliderVector;
    private float timeToCrossFade = 0.2f;
    [SerializeField] PlayerDataScriptableObject playerDataScriptableObject;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }
    private void Start()
    {
        eventManager = ServiceLocator.EventManager;
        eventManager.OnMouseSelect += EventManager_OnSelect;
        visualsManager = ServiceLocator.VisualsManager;
        visualsManager.RemoveVisual(this);
        missionManager = ServiceLocator.MissionManager;
        eventManager.OnGoToTransform += EventManager_OnGoToTransform;
        eventManager.OnPlayerAnimation += EventManager_OnPlayerAnimation;
        eventManager.OnMissionTaskEnded += EventManager_OnMissionTaskEnded;
        initialPosition = transform.position;
        playerCollider = transform.GetComponent<Collider>();
        AnimationCompleteListener animationCompleteListener = animator.GetBehaviour<AnimationCompleteListener>();
        animationCompleteListener.OnAnimationComplete += OnAnimationComplete;
        animator.CrossFade("SleepIdle", 0f);
        playerDataScriptableObject.SetPlayerObject(this);
    }
    private void Update()
    {
        //GroundedSettings();


    }
    public void OnAnimationComplete()
    {
        missionManager.EndCurrentMissiontaskCountdown();
    }
    private void EventManager_OnMissionTaskEnded(object sender, MissionTaskEventArgs e)
    {
        string playAnimation = e.missionTask.GetPlayAnimation();
        if (playAnimation != null)
        {
            //timeToCrossFade = 0.5f;
            //animator.CrossFade("Breathing Idle", timeToCrossFade);
        }
    }
    private void EventManager_OnPlayerAnimation(object sender, MissionTaskEventArgs e)
    {
        if(e.missionTask.UseTimeToCrossFade || e.missionTask.TimeToCrossFade != 0)
        {
            timeToCrossFade = e.missionTask.TimeToCrossFade;
        }
        Debug.Log("Animation: "+e.missionTask.GetPlayAnimation());
        animator.CrossFade(e.missionTask.GetPlayAnimation(), timeToCrossFade);
    }
    private void FixedUpdate()
    {
        if (!isMoving)
            return;

        Vector3 newInitialPosition = new Vector3(transform.position.x, 0.0f, transform.position.z);
        transform.position = newInitialPosition;

        Vector3 targetDirection = (targetTransform.position - transform.position).normalized;

        float rotationSpeed = 230.0f;
        bool rotationComplete = RotateTowardsDirection(targetDirection, rotationSpeed);
        if (!rotationComplete) return;

        if (IsTouchingTarget())
        {
            //timeToCrossFade = 0.2f;
            animator.CrossFade(Utils.GetString("PlayerIdleAnimation"), timeToCrossFade);
            missionManager.EndCurrentMissiontaskCountdown();
            isMoving = false;
            OnIsPlayerIdle?.Invoke(this, EventArgs.Empty);
            return;
        }
        float moveTowardsSpeed = 0.05f;
        transform.position = Vector3.MoveTowards(transform.position, targetTransform.position, moveTowardsSpeed);
        // This is an example of normalized distance based on collider size
        //float distanceToTarget = Vector3.Distance(playerCollider.ClosestPointOnBounds(transform.position), targetCollider.ClosestPointOnBounds(targetTransform.position)) - collisionRadius;
        //float normalizedDistanceToTarget = Mathf.Clamp01(distanceToTarget / initialDistanceToTarget);
        elapsedTime += Time.deltaTime;
    }

    private bool IsTouchingTarget()
    {
        // Utför en sfärisk kollisionskontroll runt spelarens position med angiven radie
        Collider[] colliders = Physics.OverlapSphere(transform.position, collisionRadius / 2f);

        // Loopa igenom alla collider som hittades
        foreach (Collider collider in colliders)
        {
            // Kontrollera om någon av kolliderarna tillhör målet
            if (collider.transform == targetTransform)
            {
                return true;
            }
        }
        return false;
    }
    private void EventManager_OnGoToTransform(object sender, MissionTaskEventArgs e)
    {
        initialPosition = transform.position;
        targetTransform = e.missionTask.GetToTransform();

        elapsedTime = 0f;
        duration = e.missionTask.TimeToExecute;
        isMoving = true;
        OnIsPlayerWalking?.Invoke(this, EventArgs.Empty);

        targetCollider = targetTransform.GetComponent<Collider>();
        if (targetCollider != null)
        {
            closestTargetPoint = targetCollider.ClosestPoint(transform.position);

            targetColliderVector = new Vector3(targetCollider.bounds.size.x, targetCollider.bounds.size.y, targetCollider.bounds.size.z);
            initialDistanceToTarget = Vector3.Distance(playerCollider.ClosestPointOnBounds(transform.position), targetCollider.ClosestPointOnBounds(targetTransform.position));
            collisionRadius = Mathf.Max(targetCollider.bounds.size.x, targetCollider.bounds.size.y, targetCollider.bounds.size.z);
        }
    }
    private bool RotateTowardsDirection(Vector3 targetDirection, float rotateSpeed)
    {
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);

        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);

        // Kontrollera om vi är nära den önskade rotationen
        float angleDifference = Quaternion.Angle(transform.rotation, targetRotation);
        float thresholdAngle = 1.0f; // Ange en tröskelvinkel för att avgöra när rotationen är klar
        if (angleDifference < thresholdAngle)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public override void StartAddon()
    {
        foreach(MissionScriptableObject mission in missionScriptableObjectList)
        {
            mission.MissionTransform = transform;
        }
    }
    public override void EventManager_OnSelect(object sender, EventArgs e)
    {
        if(WasSelected())
        {
            visualsManager.SetVisual(this);
            UpdateMissionList();
        }
        else if(Utils.WhatClickableInterfaceSelected() != null)
        {
            visualsManager.RemoveVisual(this);
        }
    }
    public override bool WasSelected()
    { 
        return Utils.WasSelected(this);
    }
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
