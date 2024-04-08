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
public class StringEventArgs : EventArgs
{
    public string StringArg { get; }
    public StringEventArgs(string stringArg)
    {
        StringArg = stringArg;
    }
    public string String => StringArg;
}
public class Vector2EventArgs : EventArgs
{
    public Vector2 Vector2Arg { get; }
    public Vector2EventArgs(Vector2 vector2Arg)
    {
        Vector2Arg = vector2Arg;
    }
    public Vector2 Vector2 => Vector2Arg;
}
public class Vector3EventArgs : EventArgs
{
    public Vector3 Vector3Arg { get; }
    public Vector3EventArgs(Vector2 vector3Arg)
    {
        Vector3Arg = vector3Arg;
    }
    public Vector3 Vector3 => Vector3Arg;
}
