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
    [SerializeField] EventVector3ArgsSO OnCameraPositionSO;
    [SerializeField] EventVector3ArgsSO OnCameraLocalPositionSO;
    [SerializeField] EventQuaternionArgsSO OnCameraRotationSO;

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
        if(!missionTask.CameraRotation.Equals(Quaternion.identity))
        {
            OnCameraRotationSO.RaiseEvent(missionTask.CameraRotation);
        }
        if(missionTask.CameraPosition != Vector3.zero)
        {
            OnCameraPositionSO.RaiseEvent(missionTask.CameraPosition);
        }
        if(missionTask.CameraLocalPosition != Vector3.zero)
        {
            OnCameraLocalPositionSO.RaiseEvent(missionTask.CameraLocalPosition);
        }
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
