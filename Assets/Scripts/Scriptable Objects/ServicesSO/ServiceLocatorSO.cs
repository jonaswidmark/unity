using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ServiceLocatorSO : MonoBehaviour
{
    public static MissionManagerSO MissionManagerSO { get; private set; }
    public static CountdownManagerSO CountdownManagerSO { get; private set; }
    public static EventManagerSO EventManagerSO { get; private set; }

    private void OnEnable()
    {
        InitializeManagers();
    }
    public static void InitializeManagers()
    {
        EventManagerSO = Resources.Load<EventManagerSO>("EventManagerSO");
        MissionManagerSO = Resources.Load<MissionManagerSO>("MissionManagerSO");
        CountdownManagerSO = Resources.Load<CountdownManagerSO>("CountdownManagerSO");
        
    }
}
