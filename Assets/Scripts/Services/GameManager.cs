using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : ServiceManager<GameManager>
{
    private StateManager stateManager;
    private EventManager eventManager;
    [SerializeField] PlayerDataScriptableObject playerDataScriptableObject;
    public enum PlayerState
    {
        Idle,
        OnMission,
    }
    private PlayerState currentState = PlayerState.Idle;
    
    private void Start()
    {
        stateManager = ServiceLocator.StateManager;
        eventManager = ServiceLocator.EventManager;
        eventManager.OnNewMissionInitialized += EventManager_OnNewMissionInitialized;
        eventManager.OnMissionEnded += EventManager_OnMissionEnded;
        eventManager.OnPlayerStatsUpdate += EventManager_OnPlayerStatsUpdate;
    }
    private void EventManager_OnPlayerStatsUpdate(object sender, EventArgs e)
    {
        Debug.Log("Game Manager: ");
    }
    private void EventManager_OnNewMissionInitialized(object sender, EventArgs e)
    {
        currentState = PlayerState.OnMission;
    }
    private void EventManager_OnMissionEnded(object sender, EventArgs e)
    {
        stateManager.SetIdleState();
    }
    
    public PlayerState GetPlayerState()
    {
        return currentState;
    }
    /* public IStateSO GetPlayerState()
    {
        currentState = stateManager.GetPlayerState();
        return currentState;
    } */
}
