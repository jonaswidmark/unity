
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
    private Rigidbody rb = null;
    public event EventHandler OnIsPlayerWalking;
    public event EventHandler OnIsPlayerIdle;
    [SerializeField] private Animator animator;
    private Transform targetTransform;
    public float duration = 15.0f; 
    private bool isMoving = false;
    public float collisionRadius = 2.0f;
    private Collider targetCollider;
    private float timeToCrossFade = 0.2f;
    [SerializeField] PlayerDataScriptableObject playerDataScriptableObject;
    [SerializeField] EventArgsSO OnPlayerStatsUpdateSO;
    private IClickable currentPlayer;
    public enum PlayerState
    {
        Idle,
        OnMission,
    }
    private PlayerState currentState = PlayerState.Idle;
    [System.Serializable]
    public struct PlayerStats
    {
        public PlayerState currentState;
    }
    public PlayerStats playerStats;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }
    private void Start()
    {
        ServiceLocatorSO.InitializeManagers();
        eventManager = ServiceLocatorSO.EventManagerSO;
        gameManager = ServiceLocatorSO.GameManagerSO;
        eventManager.OnMouseSelect += EventManager_OnSelect;
        visualsManager = ServiceLocatorSO.VisualsManagerSO;
        visualsManager.RemoveVisual(this);
        missionManager = ServiceLocatorSO.MissionManagerSO;
        eventManager.OnGoToTransform += EventManager_OnGoToTransform;
        eventManager.OnPlayerAnimation += EventManager_OnPlayerAnimation;
        eventManager.OnNewMissionInitialized += EventManager_OnNewMissionInitialized;
        eventManager.OnMissionEnded += EventManager_OnMissionEnded;
        AnimationCompleteListener animationCompleteListener = animator.GetBehaviour<AnimationCompleteListener>();
        animationCompleteListener.OnAnimationComplete += OnAnimationComplete;
        animator.CrossFade("SleepIdle", 0f);
        playerStats.currentState = currentState;
        currentPlayer = gameManager.GetPlayerByIndex(1);
        StartAddon();
    }
    public override void StartAddon()
    {
        currentPlayer = gameManager.GetPlayerByIndex(1);
        AnimationCompleteListener animationCompleteListener = animator.GetBehaviour<AnimationCompleteListener>();
        animationCompleteListener.OnAnimationComplete += OnAnimationComplete;
        animator.CrossFade("SleepIdle", 0f);
        foreach(MissionScriptableObject mission in missionScriptableObjectList)
        {
            mission.MissionTransform = transform;
        }
        StartGame();
    }
    public override void GameManager_OnStartGame(object sender, EventArgs e)
    {
        StartGame();
    }
    private void StartGame()
    {
        visualsManager.SetVisual(currentPlayer);
        UpdateMissionList();
    }
    private void EventManager_OnNewMissionInitialized(object sender, EventArgs e)
    {
        TransitionToPlayerStateOnMission();
    }
    private void EventManager_OnMissionEnded(object sender, EventArgs e)
    {
        TransitionToPlayerStateIdle();
    }
    private void UpdatePlayerStats()
    {
        playerStats.currentState = currentState;
        playerDataScriptableObject.SetPlayerStats(playerStats);
        OnPlayerStatsUpdateSO.RaiseEvent(EventArgs.Empty);
    }
    private void TransitionToPlayerStateIdle()
    {
        currentState = PlayerState.Idle;
        UpdatePlayerStats();
    }
    private void TransitionToPlayerStateOnMission()
    {
        currentState = PlayerState.OnMission;
        UpdatePlayerStats();
    }
    public PlayerState GetPlayerState()
    {
        return currentState;
    }
    public void OnAnimationComplete()
    {
        missionManager.EndCurrentMissiontaskCountdown();
    }
    private void EventManager_OnPlayerAnimation(object sender, MissionTaskEventArgs e)
    {
        if(e.missionTask.UseTimeToCrossFade || e.missionTask.TimeToCrossFade != 0)
        {
            timeToCrossFade = e.missionTask.TimeToCrossFade;
        }
        animator.CrossFade(e.missionTask.GetPlayAnimation(), timeToCrossFade);
    }
    private void FixedUpdate()
    {
        if (!isMoving)
            return;
        Vector3 newInitialPosition = new Vector3(transform.position.x, 10.0f, transform.position.z);
        transform.position = newInitialPosition;
        Vector3 targetDirection = (targetTransform.position - transform.position).normalized;
        float rotationSpeed = 230.0f;
        bool rotationComplete = RotateTowardsDirection(targetDirection, rotationSpeed);
        if (!rotationComplete) return;

        if (IsTouchingTarget())
        {
            animator.CrossFade(Utils.GetString("PlayerIdleAnimation"), timeToCrossFade);
            missionManager.EndCurrentMissiontaskCountdown();
            isMoving = false;
            OnIsPlayerIdle?.Invoke(currentPlayer, EventArgs.Empty);
            return;
        }
        float moveTowardsSpeed = 0.05f;
        transform.position = Vector3.MoveTowards(transform.position, targetTransform.position, moveTowardsSpeed);
    }

    private bool IsTouchingTarget()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, collisionRadius );
        foreach (Collider collider in colliders)
        {
            BaseSceneObject sceneObject = collider.GetComponent<BaseSceneObject>();
            if (sceneObject != null && sceneObject != gameObject && sceneObject.transform == targetTransform)
            {
                return true;
            }
        }
        return false;
    }
    private void EventManager_OnGoToTransform(object sender, MissionTaskEventArgs e)
    {
        targetTransform = e.missionTask.GetToTransformSO().GetSpawnedGameObject().transform;
        duration = e.missionTask.TimeToExecute;
        isMoving = true;
        OnIsPlayerWalking?.Invoke(currentPlayer, EventArgs.Empty);
        targetCollider = targetTransform.GetComponent<Collider>();
        if (targetCollider != null)
        {
            collisionRadius = Mathf.Max(targetCollider.bounds.size.x, targetCollider.bounds.size.y, targetCollider.bounds.size.z);
        }
    }
    private bool RotateTowardsDirection(Vector3 targetDirection, float rotateSpeed)
    {
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
        float angleDifference = Quaternion.Angle(transform.rotation, targetRotation);
        float thresholdAngle = 1.0f; 
        if (angleDifference < thresholdAngle)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public override void EventManager_OnSelect(object sender, EventArgs e)
    {
        if(WasSelected())
        {
            visualsManager.SetVisual(currentPlayer);
            UpdateMissionList();
        }
        else if(Utils.WhatClickableInterfaceSelected() != null)
        {
            visualsManager.RemoveVisual(currentPlayer);
        }
    }
    public override bool WasSelected()
    { 
        return Utils.WasSelected(this);
    }
}
