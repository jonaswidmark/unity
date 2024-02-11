using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionService : MonoBehaviour, IService
{
    private CountdownService countdownService;
    [SerializeField] private Transform parentObject;
    private MissionScriptableObject mission;
    [SerializeField] private CountdownPurpose purpose;
    private float purposeTimer = 9f;
    [SerializeField] private MissionScriptableObject missionScriptableObject;
    private Stack<CountdownPurpose> missionTasksStack = new Stack<CountdownPurpose>();
    private Stack<CountdownPurpose> reverseMissionTasksStack = new Stack<CountdownPurpose>();
    
    private void Start()
    {
        CountdownService countdownService = ServiceLocator.GetService<CountdownService>("CountdownService");
        countdownService.onMissionTaskComplete += CountdownService_onMissionTaskComplete;
    }
    public string GetServiceName()
    {
        return "MissionService";
    }
    private void CountdownService_onMissionTaskComplete(object sender, EventArgs e)
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
        
        while (missionTasksStack.Count > 0)
        {
            CountdownPurpose missionTask = missionTasksStack.Pop();
            reverseMissionTasksStack.Push(missionTask);
        }
        missionTasksStack = reverseMissionTasksStack;
        InitializeNextMissionTask();
    }
    private void InitializeNextMissionTask()
    {
        if(missionTasksStack.Count>0)
        {
            CountdownPurpose nextissionTask = missionTasksStack.Pop();
            countdownService.SpawnPrefab(purposeTimer, nextissionTask, out GameObject spawnedPrefab, out CountdownScriptableObject countDownSriptableObject);
        }
    }
}
