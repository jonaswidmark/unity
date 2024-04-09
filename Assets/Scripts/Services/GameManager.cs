using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : ServiceManager<GameManager>
{
    private IStateSO currentState;
    private StateManager stateManager;
    private EventManager eventManager;
    [SerializeField] PlayerDataScriptableObject playerDataScriptableObject;
    private void Start()
    {
        stateManager = ServiceLocator.StateManager;
        eventManager = ServiceLocator.EventManager;
        eventManager.OnMissionEnded += EventManager_OnMissionEnded;
    }
    private void EventManager_OnMissionEnded(object sender, EventArgs e)
    {
        stateManager.SetIdleState();
    }
    public IStateSO GetPlayerState()
    {
        currentState = stateManager.GetPlayerState();
        return currentState;
    }
}
