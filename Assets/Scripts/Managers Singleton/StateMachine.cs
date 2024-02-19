using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine: MonoBehaviour
{
    public static StateMachine Instance { get; private set; }
    private IState currentState;
    public event EventHandler OnMissionState;
    public event EventHandler OnIdleState;
    [SerializeField] private IdleState IdleState;
    [SerializeField] private MissionState missionState;
    private MissionManager missionManager;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.Log("There's more than one StateMachine! " + transform + " - " + Instance);
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    private void Start()
    {
        missionManager = MissionManager.Instance;
        missionManager.OnMissionEnded += MissionManager_OnMissionEnded;
        IdleState = new IdleState();
        missionState = new MissionState();
        SetCurrentState(IdleState);
    }
    private void MissionManager_OnMissionEnded(object sender, EventArgs e)
    {
        ChangeState(IdleState);
    }
    private void SetCurrentState(IState newState)
    {
        currentState = newState;
        if(newState.GetType() == typeof(MissionState))
        {
            OnMissionState?.Invoke(this, EventArgs.Empty);
        }
        if(newState.GetType() == typeof(IdleState))
        {
            OnIdleState?.Invoke(this, EventArgs.Empty);
        }
        currentState.Enter();
    }
    
    public IState GetPlayerState()
    {
        return currentState;
    }
    public void ChangeState(IState newState)
    {
        
        if (currentState != null)
        {
            currentState.Exit();
        }
        SetCurrentState(newState);
    }

    public void Update()
    {
        if (currentState != null)
        {
            currentState.Update();
        }
    }
}

