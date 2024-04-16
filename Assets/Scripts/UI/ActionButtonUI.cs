using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ActionButtonUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMeshPro;
    [SerializeField] private Button button;
    [SerializeField] private GameObject buttonContainer;
    private MissionManagerSO missionManager;
    private EventManagerSO eventManager;
    private GameManager gameManager;
    private void Start()
    {
        ServiceLocatorSO.InitializeManagers();
        gameManager = ServiceLocator.GameManager;
        eventManager = ServiceLocatorSO.EventManagerSO;
        missionManager = ServiceLocatorSO.MissionManagerSO;
        eventManager.OnNewMission += eventManager_OnNewMission;
        gameManager.OnNewMissionInitialized += gameManager_OnNewMissionInitialized;
        gameManager.OnMissionEnded += GameManager_OnMissionEnded;
        SetDisable();
    }
    private void eventManager_OnNewMission(object sender, MissionEventArgs e)
    {
        SetNewMission(e.Mission);
    }
    private void gameManager_OnNewMissionInitialized(object sender, MissionEventArgs e)
    {
        SetDisable();
    }
    private void GameManager_OnMissionEnded(object sender, MissionEventArgs e)
    {
        SetDisable();
    }
    public void SetNewMission(MissionScriptableObject missionToStart)
    {
        button.onClick.RemoveAllListeners();
        SetEnable();
        button.onClick.AddListener(() => {
                missionManager.InitializeMission();
        });
        textMeshPro.text = missionToStart.Title;
    }
    public void SetDisable()
    {
        button.interactable = false;
    }
    public void SetEnable()
    {
        button.gameObject.SetActive(true);
        button.interactable = true;
    }
}
