using System;
using UnityEngine;

public class MissionTaskEventArgs : EventArgs
{
    public MissionTask missionTask {get;}
    
    public MissionTaskEventArgs(MissionTask missionTask)
    {
        this.missionTask = missionTask;
    }
    public bool TypeText => missionTask.typeText;
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
    public Vector3EventArgs(Vector3 vector3Arg)
    {
        Vector3Arg = vector3Arg;
    }
    public Vector3 Vector3 => Vector3Arg;
}
public class QuaternionEventArgs : EventArgs
{
    public Quaternion QuaternionArg { get; }
    public QuaternionEventArgs(Quaternion quaternion3Arg)
    {
        QuaternionArg = quaternion3Arg;
    }
    public Quaternion Quaternion => QuaternionArg;
}
