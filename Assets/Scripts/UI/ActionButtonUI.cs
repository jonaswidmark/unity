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

    private void Start()
    {
        missionManager = ServiceLocator.MissionManager;
        missionManager.OnNewMission += missionManager_OnNewMission;
        missionManager.OnNewMissionInitialized += missionManager_OnNewMissionInitialized;
        missionManager.OnMissionEnded += MissionManager_OnMissionEnded;
        SetDisable();
    }
    private void missionManager_OnNewMission(object sender, MissionEventArgs e)
    {
        SetNewMission(e.Mission);
    }
    private void missionManager_OnNewMissionInitialized(object sender, EventArgs e)
    {
        SetDisable();
    }
    private void MissionManager_OnMissionEnded(object sender, EventArgs e)
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
