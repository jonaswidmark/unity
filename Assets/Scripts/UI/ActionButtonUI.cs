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
    private MissionManager missionManager;
    private EventManager eventManager;

    private void Start()
    {
        eventManager = ServiceLocator.EventManager;
        missionManager = ServiceLocator.MissionManager;
        eventManager.OnNewMission += eventManager_OnNewMission;
        eventManager.OnNewMissionInitialized += eventManager_OnNewMissionInitialized;
        eventManager.OnMissionEnded += EventManager_OnMissionEnded;
        SetDisable();
    }
    private void eventManager_OnNewMission(object sender, MissionEventArgs e)
    {
        SetNewMission(e.Mission);
    }
    private void eventManager_OnNewMissionInitialized(object sender, EventArgs e)
    {
        SetDisable();
    }
    private void EventManager_OnMissionEnded(object sender, EventArgs e)
    {
        SetDisable();
    }
    public void SetNewMission(MissionScriptableObject missionToStart)
    {
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
