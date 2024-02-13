using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MissionManager : MonoBehaviour
{
    public static MissionManager Instance { get; private set; }
    public event EventHandler<MissionEventArgs> OnMissionEnded;

    public event EventHandler<MissionEventArgs> OnUpdatedMissionList;
    private CountdownManager countdownManager;
    [SerializeField] private Transform parentObject;
    private MissionScriptableObject activeMission;
    [SerializeField] private CountdownPurpose purpose;
    private float purposeTimer = 9f;
    [SerializeField] private List<MissionScriptableObject> missionScriptableObjectList = new List<MissionScriptableObject>();
    private Stack<CountdownPurpose> missionTasksStack = new Stack<CountdownPurpose>();
    private Stack<CountdownPurpose> reverseMissionTasksStack = new Stack<CountdownPurpose>();
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
    private void SetActiveMission(MissionScriptableObject activeMission)
    {
        this.activeMission = activeMission;
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
            // Om listan är tom, hantera detta scenario här
            Debug.Log("Inga tillgängliga missioner hittades.");
        }
    }
    private void Start()
    {
        countdownManager = CountdownManager.Instance;
        countdownManager.onMissionTaskComplete += CountdownManager_onMissionTaskComplete;
        UpdateMissionList();
       
    }
    private void CountdownManager_onMissionTaskComplete(object sender, EventArgs e)
    {
        InitializeNextMissionTask();
    }
    
    public void InitializeMission()
    {
        List<ScriptableObject> missionTasks = activeMission.GetMissionTasks();
        missionTasksStack.Clear();
        
        foreach(CountdownPurpose missionTask in missionTasks)
        {
            missionTasksStack.Push(missionTask);
        }
        
        InitializeNextMissionTask();
    }
    private void InitializeNextMissionTask()
    {
        if(missionTasksStack.Count>0)
        {
            CountdownPurpose nextissionTask = missionTasksStack.Pop();
            countdownManager.SpawnPrefab(purposeTimer, nextissionTask, out GameObject spawnedPrefab, out CountdownScriptableObject countDownSriptableObject);
        }
        else
        {
            MissionEventArgs eventArgs = new MissionEventArgs(activeMission);
            OnMissionEnded?.Invoke(this, eventArgs);
        }
    }
}
