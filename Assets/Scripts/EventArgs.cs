using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionTaskEventArgs : EventArgs
{
    public MissionTask missionTask {get;}
    public MissionTaskEventArgs(MissionTask missionTask)
    {
        this.missionTask = missionTask;
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
/* 
public class IStateArgs : EventArgs
{
    public IStateArgs IStateSO { get; }
    public IStateArgs(IStateSO iStateSO)
    {
        IStateSO = (IStateArgs)iStateSO;
    }
    
} */
