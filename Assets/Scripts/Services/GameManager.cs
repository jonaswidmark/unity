using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : ServiceManager<GameManager>
{
    private IClickable currentPlayer;
    private EventManagerSO eventManager;
    [SerializeField] EventArgsSO OnStartGameSO;
    public event EventHandler OnStartGame;
    public event EventHandler<MissionEventArgs> OnNewMissionInitialized;
    public event EventHandler<MissionEventArgs> OnMissionEnded;
    public event EventHandler<EventArgs> OnPlayerStatsUpdate;
    [SerializeField] PlayerDataScriptableObject playerDataScriptableObject;
    [SerializeField] SceneMainObjectScriptableObject[] sceneMainObjectsSO;
    public enum PlayerState
    {
        Idle,
        OnMission,
    }
    private string currentState = "Idle";
    private void Awake()
    {
        SetSceneObject();
    }
    private void OnEnable()
    {
        Debug.Log("GameManager OnEnable");
        ServiceLocatorSO.InitializeManagers();
        eventManager = ServiceLocatorSO.EventManagerSO;
        eventManager.OnNewMissionInitialized += EventManager_OnNewMissionInitialized;
        eventManager.OnMissionEnded += EventManager_OnMissionEnded;
        eventManager.OnPlayerStatsUpdate += EventManager_OnPlayerStatsUpdate;
        eventManager.OnStartGame += OnStartGameSO_OnStartGame;
    }
    
    private void OnStartGameSO_OnStartGame(object sender, EventArgs e)
    {
        
        Debug.Log("GM starts game");
        OnStartGame?.Invoke(this, e);
    }
    public string GetCurrentPlayerState()
    {
        return currentState;
    }
    private void EventManager_OnPlayerStatsUpdate(object sender, EventArgs e)
    {
        currentState = playerDataScriptableObject.GetPlayerStats().currentState.ToString();
        OnPlayerStatsUpdate?.Invoke(sender, e);
    }
    private void EventManager_OnNewMissionInitialized(object sender, MissionEventArgs e)
    {
        /** Invoked when a new mission is initialized **/
        OnNewMissionInitialized?.Invoke(sender, e);
    }
    private void EventManager_OnMissionEnded(object sender, MissionEventArgs e)
    {
        OnMissionEnded?.Invoke(sender, e);
        //stateManager.SetIdleState();
    }
    /* public IStateSO GetPlayerState()
    {
        currentState = stateManager.GetPlayerState();
        return currentState;
    } */
    private void SetSceneObject()
    {
        foreach(SceneMainObjectScriptableObject sceneMainObjectSO in sceneMainObjectsSO)
        {
            GameObject prefabGO = sceneMainObjectSO.GetThisGameObject();
            
            sceneMainObjectSO.SpawnPrefab(prefabGO);
        }
    }
    public IClickable GetPlayerByIndex(int index)
    {
        foreach(SceneMainObjectScriptableObject sceneMainObjectsSO in sceneMainObjectsSO)
        {
            if(sceneMainObjectsSO.GetIfPlayerIndex() == index)
            {
                currentPlayer = sceneMainObjectsSO.GetSpawnedObjectAsClickableInterface();
            }
        } 
        return currentPlayer;
    }
}
