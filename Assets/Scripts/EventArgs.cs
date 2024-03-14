using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskEventArgs : EventArgs
{
    public MissionTask missionTask {get;}
    public float timer {get;}

    public TaskEventArgs(float timer, MissionTask missionTask)
    {
        this.missionTask = missionTask;
        this.timer = timer;
    }
}

public class MissionEventArgs : EventArgs
{
    public MissionScriptableObject Mission { get; }

    public MissionEventArgs(MissionScriptableObject mission)
    {
        Mission = mission;
    }

    public string Title => Mission.Title;
}
