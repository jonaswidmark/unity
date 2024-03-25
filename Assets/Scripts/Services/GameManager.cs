using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : ServiceManager<GameManager>
{
    private IStateSO currentState;
    private StateManager stateManager;
    private MissionManager missionManager;
    [SerializeField] Transform actionButton;
    private ActionButtonUI actionButtonUI;
    
    private void Start()
    {
        stateManager = ServiceLocator.StateManager;
        missionManager = ServiceLocator.MissionManager;
        missionManager.OnMissionEnded += MissionManager_OnMissionEnded;
        missionManager.OnNewMission += missionManager_OnNewMission;
        missionManager.OnNewMissionInitialized += missionManager_OnNewMissionInitialized;
        actionButtonUI = actionButton.GetComponent<ActionButtonUI>();
    }
    private void missionManager_OnNewMissionInitialized(object sender, EventArgs e)
    {
        actionButtonUI.SetDisable();
    }
    private void missionManager_OnNewMission(object sender, MissionEventArgs e)
    {
        actionButtonUI.SetNewMission(e.Mission);
      
    }
    private void MissionManager_OnMissionEnded(object sender, EventArgs e)
    {
        stateManager.SetIdleState();
        actionButtonUI.SetDisable();
    }
    public IStateSO GetPlayerState()
    {
        currentState = stateManager.GetPlayerState();
        return currentState;
    }
}
