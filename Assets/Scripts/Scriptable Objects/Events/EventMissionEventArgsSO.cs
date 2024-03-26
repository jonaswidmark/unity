using UnityEngine;
using System;

[CreateAssetMenu(fileName = "New MissionEventArgs Event", menuName = "Events/MissionEventArgs")]
public class EventMissionEventArgsSO : ScriptableObject
{
    public event EventHandler<MissionEventArgs> OnRaiseMissionEvent;

    public void RaiseEvent(MissionScriptableObject mission)
    {
        OnRaiseMissionEvent?.Invoke(this, new MissionEventArgs(mission));
    }
}