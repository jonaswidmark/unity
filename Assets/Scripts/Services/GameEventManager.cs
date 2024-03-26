using System;
using UnityEngine;

public class GameEventManager : MonoBehaviour
{
    [SerializeField] GameEvent<EventArgs> onMouseSelectEvent;
    [SerializeField] GameEvent<EventArgs> onCountdownOrCallbackCompleteEvent;
    [SerializeField] GameEvent<MissionTaskEventArgs> onMissionTaskEndedEvent;
    [SerializeField] GameEvent<MissionTaskEventArgs> onGoToTransformEvent;
    [SerializeField] GameEvent<MissionTaskEventArgs> onPlayerAnimationEvent;
    [SerializeField] GameEvent<MissionEventArgs> onMissionEndedEvent;
    [SerializeField] GameEvent<MissionEventArgs> onNewMissionEvent;
    [SerializeField] GameEvent<MissionEventArgs> onNewMissionInitializedEvent;

    private void Awake()
    {
        // Skapa ett nytt GameEvent-objekt
        onMouseSelectEvent = ScriptableObject.CreateInstance<GameEvent<EventArgs>>();
        Debug.Log("GameEventManager: "+ onMouseSelectEvent);
    }
}