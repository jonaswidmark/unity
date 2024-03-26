using UnityEngine;
using System;

[CreateAssetMenu(fileName = "New MissionTaskEventArgs Event", menuName = "Events/MissionTaskEventArgs")]
public class EventMissionTaskEventArgsSO : ScriptableObject
{
    public event EventHandler<MissionTaskEventArgs> OnRaiseMissionTaskEvent;

    public void RaiseEvent(MissionTask missionTask)
    {
        OnRaiseMissionTaskEvent?.Invoke(this, new MissionTaskEventArgs(missionTask));
    }
}