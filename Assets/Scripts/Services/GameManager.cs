using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : ServiceManager<GameManager>
{
    private StateManager stateManager;
    private EventManager eventManager;
    public event EventHandler<MissionEventArgs> OnNewMissionInitialized;
    public event EventHandler<MissionEventArgs> OnMissionEnded;
    public event EventHandler<EventArgs> OnPlayerStatsUpdate;
    [SerializeField] PlayerDataScriptableObject playerDataScriptableObject;
    public enum PlayerState
    {
        Idle,
        OnMission,
    }
    private string currentState = "Idle";
    
    private void Start()
    {
        stateManager = ServiceLocator.StateManager;
        eventManager = ServiceLocator.EventManager;
        eventManager.OnNewMissionInitialized += EventManager_OnNewMissionInitialized;
        eventManager.OnMissionEnded += EventManager_OnMissionEnded;
        eventManager.OnPlayerStatsUpdate += EventManager_OnPlayerStatsUpdate;
    }
    public string GetCurrentPlayerState()
    {
        return currentState;
    }
    private void EventManager_OnPlayerStatsUpdate(object sender, EventArgs e)
    {
        currentState = playerDataScriptableObject.GetPlayerStats().currentState.ToString();
        OnPlayerStatsUpdate?.Invoke(sender, e);
    }
    private void EventManager_OnNewMissionInitialized(object sender, MissionEventArgs e)
    {
        /** Invoked when a new mission is initialized **/
        OnNewMissionInitialized?.Invoke(sender, e);
    }
    private void EventManager_OnMissionEnded(object sender, MissionEventArgs e)
    {
        OnMissionEnded?.Invoke(sender, e);
        //stateManager.SetIdleState();
    }
    /* public IStateSO GetPlayerState()
    {
        currentState = stateManager.GetPlayerState();
        return currentState;
    } */
}
