using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MissionManager : MonoBehaviour, IService
{
    public static MissionManager Instance { get; private set; }
    public event EventHandler<MissionEventArgs> OnMissionEnded;
    public event EventHandler<MissionEventArgs> OnUpdatedMissionList;
    public event EventHandler<MissionTaskEventArgs> OnGoToTransform;
    public event EventHandler<MissionEventArgs> OnNewMission;
    private CountdownManager countdownManager;
    [SerializeField] private Transform parentObject;
    private MissionScriptableObject activeMission;
    private MissionTask activeMissionTask;
    [SerializeField] private List<MissionScriptableObject> missionScriptableObjectList = new List<MissionScriptableObject>();
    private Stack<MissionTask> missionTasksStack = new Stack<MissionTask>();
    private MissionTask nextissionTask;
    private void Awake()
    {
        if (Instance != null)
        {
            Debug.Log("There's more than one MissionManager! " + transform + " - " + Instance);
            Destroy(gameObject);
            return;
        }
        Instance = this;
        
    }
    public string GetServiceName()
    {
        return "MissionManager";
    }
    private void SetActiveMission(MissionScriptableObject activeMission)
    {
        this.activeMission = activeMission;
    }
    public void SetNewMissionAction(MissionScriptableObject mission)
    {
        SetActiveMission(mission);
        MissionEventArgs eventArgs = new MissionEventArgs(mission);
        OnNewMission?.Invoke(this,eventArgs);
    }
    public void UpdateMissionList()
    {
        var availableMissions = missionScriptableObjectList.Where(mission => mission.isAvailable);
        var sortedMissions = availableMissions.OrderBy(mission => mission.missionOrder);
        var firstMission = sortedMissions.FirstOrDefault();
        
        if (firstMission != null)
        {
            SetActiveMission(firstMission);
            MissionEventArgs eventArgs = new MissionEventArgs(firstMission);
            OnUpdatedMissionList?.Invoke(this,eventArgs);
        }
        else
        {
            
            Debug.Log("Inga tillgängliga missioner hittades.");
        }
    }
    private void Start()
    {
        countdownManager = CountdownManager.Instance;
        countdownManager.onMissionTaskComplete += CountdownManager_onMissionTaskComplete;
    }
    private void CountdownManager_onMissionTaskComplete(object sender, EventArgs e)
    {
        InitializeNextMissionTask();
    }
    
    public void InitializeMission()
    {
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
            countdownManager.SpawnPrefab(timeToExecute, nextissionTask, out GameObject spawnedPrefab, out CountdownScriptableObject countDownSriptableObject);
            InvokeMissionTaskEvents(nextissionTask);
        }
        else
        {
            EndCurrentMission();
        }
    }
    private void InvokeMissionTaskEvents(MissionTask missionTask)
    {
        Transform goToTransform = missionTask.GetToTransform();
        if(goToTransform != null)
        {
            MissionTaskEventArgs eventArgs = new MissionTaskEventArgs(missionTask);
            OnGoToTransform?.Invoke(this, eventArgs);
        } 
    }
    public void EndCurrentMissiontaskCountdown()
    {
        nextissionTask.GetActiveCountdown().EndCountDown();
    }
    public void EndCurrentMission()
    {
        MissionEventArgs eventArgs = new MissionEventArgs(activeMission);
        OnMissionEnded?.Invoke(this, eventArgs);
    }
}
