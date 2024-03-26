using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : ServiceManager<EventManager>
{
    [SerializeField] EventArgsSO OnMouseSelectSO;
    public event EventHandler OnMouseSelect;
    [SerializeField] EventArgsSO OnCountDownOrCallbackComplete;
    public event EventHandler OnMissionTaskComplete;
    [SerializeField] EventMissionTaskEventArgsSO OnMissionTaskEndedSO;
    public event EventHandler<MissionTaskEventArgs> OnMissionTaskEnded;
    public event EventHandler<MissionTaskEventArgs> OnGoToTransform;
    [SerializeField] EventMissionTaskEventArgsSO OnGoToTransformSO;
    public event EventHandler<MissionTaskEventArgs> OnPlayerAnimation;
    [SerializeField] EventMissionTaskEventArgsSO OnPlayerAnimationSO;
    public event EventHandler<MissionEventArgs> OnMissionEnded;
    [SerializeField] EventMissionEventArgsSO OnMissionEndedSO;
    public event EventHandler<MissionEventArgs> OnNewMission;
    [SerializeField] EventMissionEventArgsSO OnNewMissionSO;
    public event EventHandler<MissionEventArgs> OnNewMissionInitialized;
    [SerializeField] EventMissionEventArgsSO OnNewMissionInitializedSO;
    private void Start()
    {
        OnMouseSelectSO.OnRaiseEvent += OnMouseSelectSO_OnRaiseEvent;
        OnCountDownOrCallbackComplete.OnRaiseEvent += OnCountDownOrCallbackComplete_OnRaiseEvent;
        OnGoToTransformSO.OnRaiseMissionTaskEvent += OnGoToTransformSO_OnRaiseMissionTaskEvent;
        OnPlayerAnimationSO.OnRaiseMissionTaskEvent += OnPlayerAnimationSO_OnRaiseMissionTaskEvent;
        OnMissionTaskEndedSO.OnRaiseMissionTaskEvent += OnMissionTaskEndedSO_OnRaiseMissionTaskEvent;
        OnMissionEndedSO.OnRaiseMissionEvent += OnMissionEndedSO_OnRaiseMissionEvent;
        OnNewMissionSO.OnRaiseMissionEvent += OnNewMissionSO_OnRaiseMissionEvent;
        OnNewMissionInitializedSO.OnRaiseMissionEvent += OnNewMissionInitializedSO_OnRaiseMissionEvent;
    }
    /** Hierarchy: countdown (countdown or callback) -> mission task -> mission**/
    private void OnMouseSelectSO_OnRaiseEvent(object sender, EventArgs e)
    {
        /** Left click mouse in the scene, selecting objekts **/
        OnMouseSelect?.Invoke(sender, EventArgs.Empty);
    }
    private void OnCountDownOrCallbackComplete_OnRaiseEvent(object sender, EventArgs e)
    {
        /** Attached to a Mission task. can be a countdown or a callback **/
        OnMissionTaskComplete?.Invoke(sender, EventArgs.Empty);
    }
    private void OnMissionTaskEndedSO_OnRaiseMissionTaskEvent(object sender, MissionTaskEventArgs e)
    {
        /** Mission task, as part of a mission **/
        OnMissionTaskEnded?.Invoke(sender, e);
    }
    private void OnGoToTransformSO_OnRaiseMissionTaskEvent(object sender, MissionTaskEventArgs e)
    {
        /** If mission task is a desplacement, go to transform **/
        OnGoToTransform?.Invoke(sender, e);
    }
    private void OnPlayerAnimationSO_OnRaiseMissionTaskEvent(object sender, MissionTaskEventArgs e)
    {
        /** Invoked when a mision task has a player animation attached (as a string name on the scriptable object) **/
        OnPlayerAnimation?.Invoke(sender, e);
    }
    private void OnMissionEndedSO_OnRaiseMissionEvent(object sender, MissionEventArgs e)
    {
        /** Invoked when all mission tasks on a mission ended **/
        OnMissionEnded?.Invoke(sender, e);
    }
    private void OnNewMissionSO_OnRaiseMissionEvent(object sender, MissionEventArgs e)
    {
        /** Invoke when a game object with a mission attached is selected, before initializing the mission **/
        OnNewMission?.Invoke(sender, e);
    }
    private void OnNewMissionInitializedSO_OnRaiseMissionEvent(object sender, MissionEventArgs e)
    {
        /** Invoked when a new mission is initialized **/
        OnNewMissionInitialized?.Invoke(sender, e);
    }
}
