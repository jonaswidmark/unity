using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : ServiceManager<GameManager>
{
    private IStateSO currentState;
    private StateManager stateManager;
    private MissionManager missionManager;
    private void Start()
    {
        stateManager = ServiceLocator.StateManager;
        missionManager = ServiceLocator.MissionManager;
        missionManager.OnMissionEnded += MissionManager_OnMissionEnded;
       
    }
    private void MissionManager_OnMissionEnded(object sender, EventArgs e)
    {
        stateManager.SetIdleState();
    }
    public IStateSO GetPlayerState()
    {
        currentState = stateManager.GetPlayerState();
        return currentState;
    }
}
