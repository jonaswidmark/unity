using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionManager : MonoBehaviour
{
    public static MissionManager Instance { get; private set; }
    private CountdownManager countdownManager;
    [SerializeField] private Transform parentObject;
    private MissionScriptableObject mission;
    [SerializeField] private CountdownPurpose purpose;
    private float purposeTimer = 9f;
    [SerializeField] private MissionScriptableObject missionScriptableObject;
    private Stack<CountdownPurpose> missionTasksStack = new Stack<CountdownPurpose>();
    private Stack<CountdownPurpose> reverseMissionTasksStack = new Stack<CountdownPurpose>();
    private void Awake()
    {
        if (Instance != null)
        {
            Debug.Log("There's more than one MonoBehaviour! " + transform + " - " + Instance);
            Destroy(gameObject);
            return;
        }
        Instance = this;
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
    public void InitializeMission()//MissionScriptableObject mission)
    {
        List<CountdownPurpose> missionTasks = missionScriptableObject.GetMissionTasks();
        
        foreach(CountdownPurpose missionTask in missionTasks)
        {
            missionTasksStack.Push(missionTask);
        }
        
        /* while (missionTasksStack.Count > 0)
        {
            CountdownPurpose missionTask = missionTasksStack.Pop();
            reverseMissionTasksStack.Push(missionTask);
        } */
        InitializeNextMissionTask();
    }
    private void InitializeNextMissionTask()
    {
        if(missionTasksStack.Count>0)
        {
            CountdownPurpose nextissionTask = missionTasksStack.Pop();
            countdownManager.SpawnPrefab(purposeTimer, nextissionTask, out GameObject spawnedPrefab, out CountdownScriptableObject countDownSriptableObject);
        }
    }
}
