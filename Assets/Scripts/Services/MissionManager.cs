using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MissionManager : ServiceManager<MissionManager>
{
    [SerializeField] EventMissionEventArgsSO OnMissionEndedSO;
    //public event EventHandler<MissionEventArgs> OnUpdatedMissionList;
    [SerializeField] EventMissionTaskEventArgsSO OnGoToTransformSO;
    [SerializeField] EventMissionTaskEventArgsSO OnPlayerAnimationSO;
    [SerializeField] EventMissionTaskEventArgsSO OnMissionTaskEndedSO;
    [SerializeField] EventMissionEventArgsSO OnNewMissionSO;
    [SerializeField] EventMissionEventArgsSO OnNewMissionInitializedSO;
    private EventManager eventManager;
    private CountdownManager countdownManager;
    [SerializeField] private Transform parentObject;
    private MissionScriptableObject activeMission;
    private MissionTask activeMissionTask;
    [SerializeField] private List<MissionScriptableObject> missionScriptableObjectList = new List<MissionScriptableObject>();
    private Stack<MissionTask> missionTasksStack = new Stack<MissionTask>();
    private MissionTask nextissionTask;
    private MissionTask currentMissionTask;
    private void Start()
    {
        eventManager = ServiceLocator.EventManager;
        eventManager.OnMissionTaskComplete += EventManager_OnMissionTaskComplete;
        countdownManager = ServiceLocator.CountdownManager;
    }
    private void SetActiveMission(MissionScriptableObject activeMission)
    {
        this.activeMission = activeMission;
    }
    public void SetNewMissionAction(MissionScriptableObject mission)
    {
        Debug.Log("Set mission :  " + mission);
        SetActiveMission(mission);
        OnNewMissionSO.RaiseEvent(mission);
    }
    public void UpdateMissionList()
    {
        var availableMissions = missionScriptableObjectList.Where(mission => mission.isAvailable);
        var sortedMissions = availableMissions.OrderBy(mission => mission.missionOrder);
        var firstMission = sortedMissions.FirstOrDefault();
        
        if (firstMission != null)
        {
            SetNewMissionAction(firstMission);
            Debug.Log("Updated mission list :  " + firstMission);
            //MissionEventArgs eventArgs = new MissionEventArgs(firstMission);
            //OnUpdatedMissionList?.Invoke(this,eventArgs);
        }
        else
        {
            Debug.Log("Inga tillgängliga missioner hittades.");
        }
    }
    private void EventManager_OnMissionTaskComplete(object sender, EventArgs e)
    {
        OnMissionTaskEndedSO.RaiseEvent(currentMissionTask);
        InitializeNextMissionTask();
    }
    
    public void InitializeMission()
    {
        Debug.Log("Mission: " +activeMission + " activated!");
        OnNewMissionInitializedSO.RaiseEvent(activeMission);
        List<ScriptableObject> missionTasks = activeMission.GetMissionTasks();
        missionTasksStack.Clear();
        foreach(MissionTask missionTask in missionTasks)
        {
            missionTasksStack.Push(missionTask);
        }
        InitializeNextMissionTask();
    }
    private void InitializeNextMissionTask()
    {
        if(missionTasksStack.Count>0)
        {
            nextissionTask = missionTasksStack.Pop();
            float timeToExecute = nextissionTask.TimeToExecute;
            countdownManager.SpawnPrefab(timeToExecute, nextissionTask, out GameObject spawnedPrefab, out CountdownScriptableObject countDownSriptableObject, nextissionTask.Placing);
            InvokeMissionTaskEvents(nextissionTask);
        }
        else
        {
            EndCurrentMission();
        }
    }
    private void InvokeMissionTaskEvents(MissionTask missionTask)
    {
        currentMissionTask = missionTask;
        MissionTaskEventArgs eventArgs = new MissionTaskEventArgs(missionTask);
        
        Transform goToTransform = missionTask.GetToTransform();
        if(goToTransform != null)
        {
            OnGoToTransformSO.RaiseEvent(eventArgs.missionTask);
        } 
        string playAnimation = missionTask.GetPlayAnimation();
        if(!string.IsNullOrEmpty(playAnimation))
        {
            OnPlayerAnimationSO.RaiseEvent(eventArgs.missionTask);
        }
    }
    public void EndCurrentMissiontaskCountdown()
    {
        
        nextissionTask.GetActiveCountdown().EndCountDown();
    }
    public void EndCurrentMission()
    {
        MissionEventArgs eventArgs = new MissionEventArgs(activeMission);
        OnMissionEndedSO.RaiseEvent(eventArgs.Mission);
    }
}
