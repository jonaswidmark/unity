using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : ServiceManager<EventManager>
{
    private InputManager inputManager;
    public event EventHandler OnMouseSelect;
    private CountdownManager countdownManager;
    public event EventHandler OnMissionTaskComplete;
    private MissionManager missionManager;
    public event EventHandler<MissionTaskEventArgs> OnMissionTaskEnded;
    public event EventHandler<MissionTaskEventArgs> OnGoToTransform;
    public event EventHandler<MissionTaskEventArgs> OnPlayerAnimation;
    public event EventHandler<MissionEventArgs> OnMissionEnded;
    public event EventHandler<MissionEventArgs> OnNewMission;
    public event EventHandler<MissionEventArgs> OnNewMissionInitialized;
    private void Start()
    {
        inputManager = ServiceLocator.InputManager;
        inputManager = ServiceLocator.InputManager;
        inputManager.OnMouseSelect += InputManager_OnSelect;
        countdownManager = ServiceLocator.CountdownManager;
        countdownManager.OnMissionTaskComplete += CountdownManager_OnMissionTaskComplete;
        missionManager = ServiceLocator.MissionManager;
        missionManager.OnGoToTransform += MissionManager_OnGoToTransform;
        missionManager.OnPlayerAnimation += MissionManager_OnPlayerAnimation;
        missionManager.OnMissionTaskEnded += MissionManager_OnMissionTaskEnded;
        missionManager.OnMissionEnded += MissionManager_OnMissionEnded;
        missionManager.OnNewMission += MissionManager_OnNewMission;
        missionManager.OnNewMissionInitialized += MissionManager_OnNewMissionInitialized;
    }
    private void InputManager_OnSelect(object sender, EventArgs e)
    {
        /** Left click mouse in the scene, selecting objekts **/
        OnMouseSelect?.Invoke(sender, EventArgs.Empty);
    }
    private void CountdownManager_OnMissionTaskComplete(object sender, EventArgs e)
    {
        /** Mission task, as part of a mission**/
        OnMissionTaskComplete?.Invoke(sender, EventArgs.Empty);
    }
    private void MissionManager_OnMissionTaskEnded(object sender, MissionTaskEventArgs e)
    {
        OnMissionTaskEnded?.Invoke(sender, e);
    }
    private void MissionManager_OnGoToTransform(object sender, MissionTaskEventArgs e)
    {
        OnGoToTransform?.Invoke(sender, e);
    }
    private void MissionManager_OnPlayerAnimation(object sender, MissionTaskEventArgs e)
    {
        OnPlayerAnimation?.Invoke(sender, e);
    }
    private void MissionManager_OnMissionEnded(object sender, MissionEventArgs e)
    {
        OnMissionEnded?.Invoke(sender, e);
    }
    private void MissionManager_OnNewMission(object sender, MissionEventArgs e)
    {
        OnNewMission?.Invoke(sender, e);
    }
    private void MissionManager_OnNewMissionInitialized(object sender, MissionEventArgs e)
    {
        OnNewMissionInitialized?.Invoke(sender, e);
    }
}
