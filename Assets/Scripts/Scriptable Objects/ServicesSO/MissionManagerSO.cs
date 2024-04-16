using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(fileName = "MissionManagerSO", menuName = "ServicesSO/MissionManagerSO")]
public class MissionManagerSO : ScriptableObject
{
    [SerializeField] EventMissionEventArgsSO OnMissionEndedSO;
    [SerializeField] EventMissionTaskEventArgsSO OnGoToTransformSO;
    [SerializeField] EventMissionTaskEventArgsSO OnPlayerAnimationSO;
    [SerializeField] EventMissionTaskEventArgsSO OnMissionTaskEndedSO;
    [SerializeField] EventMissionEventArgsSO OnNewMissionSO;
    [SerializeField] EventMissionEventArgsSO OnNewMissionInitializedSO;
    [SerializeField] EventVector3ArgsSO OnCameraPositionSO;
    [SerializeField] EventVector3ArgsSO OnCameraLocalPositionSO;
    [SerializeField] EventQuaternionArgsSO OnCameraRotationSO;
    [SerializeField] EventMissionTaskEventArgsSO OnToggleAlertArrowSO;
    private EventManagerSO eventManager;
    private CountdownManagerSO countdownManager;
    private MissionScriptableObject activeMission;
    private MissionTask activeMissionTask;
    private List<MissionScriptableObject> missionScriptableObjectList = new List<MissionScriptableObject>();
    private Stack<MissionTask> missionTasksStack = new Stack<MissionTask>();
    private MissionTask nextMissionTask;
    private MissionTask currentMissionTask;
    private void OnEnable()
    {
        ServiceLocatorSO.InitializeManagers();
        countdownManager = ServiceLocatorSO.CountdownManagerSO;
        eventManager = ServiceLocatorSO.EventManagerSO;
        eventManager.OnMissionTaskComplete += EventManager_OnMissionTaskComplete;
    }
    private void SetActiveMission(MissionScriptableObject activeMission)
    {
        this.activeMission = activeMission;
    }
    public void SetNewMissionAction(MissionScriptableObject mission)
    {
        SetActiveMission(mission);
        Debug.Log("Mission ManagerSO SetNewMissionAction");
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
        foreach (MissionTask missionTask in missionTasks)
        {
            missionTasksStack.Push(missionTask);
        }
        InitializeNextMissionTask();
    }
    private void InitializeNextMissionTask()
    {
        if (missionTasksStack.Count > 0)
        {
            nextMissionTask = missionTasksStack.Pop();
            float timeToExecute = nextMissionTask.TimeToExecute;
            countdownManager.SpawnPrefab(timeToExecute, nextMissionTask, out GameObject spawnedPrefab, out CountdownScriptableObject countDownSriptableObject, nextMissionTask.Placing);
            InvokeMissionTaskEvents(nextMissionTask);
        }
        else
        {
            EndCurrentMission();
        }
    }
    private void InvokeMissionTaskEvents(MissionTask missionTask)
    {
        currentMissionTask = missionTask;
        string playAnimation = missionTask.GetPlayAnimation();
        MissionTaskEventArgs eventArgs = new MissionTaskEventArgs(missionTask);
        Transform goToTransform = missionTask.GetToTransform();
        if (missionTask.CameraRotation.x != 0 ||
            missionTask.CameraRotation.y != 0 ||
            missionTask.CameraRotation.z != 0)
        {
            OnCameraRotationSO.RaiseEvent(missionTask.CameraRotation);
        }
        if (missionTask.CameraPosition != Vector3.zero)
        {
            OnCameraPositionSO.RaiseEvent(missionTask.CameraPosition);
        }
        if (missionTask.CameraLocalPosition != Vector3.zero)
        {
            OnCameraLocalPositionSO.RaiseEvent(missionTask.CameraLocalPosition);
        }
        if (goToTransform != null && missionTask.GetTransformAction() == MissionTask.TransformAction.playerGoTo)
        {
            OnGoToTransformSO.RaiseEvent(eventArgs.missionTask);
        }
        if (goToTransform != null && missionTask.GetTransformAction() == MissionTask.TransformAction.alertArrow)
        {
            OnToggleAlertArrowSO.RaiseEvent(eventArgs.missionTask);
        }
        if (!string.IsNullOrEmpty(playAnimation))
        {
            OnPlayerAnimationSO.RaiseEvent(eventArgs.missionTask);
        }
    }
    public void EndCurrentMissiontaskCountdown()
    {
        nextMissionTask.GetActiveCountdown().EndCountDown();
    }
    public void EndCurrentMission()
    {
        MissionEventArgs eventArgs = new MissionEventArgs(activeMission);
        foreach (MissionScriptableObject missionScriptableObject in activeMission.MissionsAvailable)
        {
            missionScriptableObject.IsAvailable = true;
        }
        OnMissionEndedSO.RaiseEvent(eventArgs.Mission);
    }
}
