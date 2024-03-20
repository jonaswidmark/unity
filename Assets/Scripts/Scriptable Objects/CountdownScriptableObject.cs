using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Linq;

[CreateAssetMenu(fileName = "New Countdown", menuName = "CustomObjects/Countdown")]
public class CountdownScriptableObject : ScriptableObject
{
    private MissionTask missionTask;
    private bool timerIsRunning = false;
    private float timeRemaining = 0f;
    public delegate void CountdownFinishedEvent(CountdownScriptableObject countdownData);
    public event CountdownFinishedEvent OnCountdownFinished;
    
    public float TimeRemaining
    {
        get { return timeRemaining; }
    }
    public void SetCountdownMissionTask(MissionTask missionTask)
    {
        this.missionTask = missionTask;
    }
    public MissionTask GetCountdownMissionTask()
    {
        return missionTask;
    }
    public void StartCountdown(float initialTime)
    {    
        timerIsRunning = true;
        timeRemaining = initialTime;
    }
    public void UpdateCountdown()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;       
            }
            else
            {
                OnCountdownFinished?.Invoke(this);
                timeRemaining = 0;
                timerIsRunning = false;
            }
        }
    }
    public float GetTimeRemaining()
    {
        return timeRemaining;
    } 
}
