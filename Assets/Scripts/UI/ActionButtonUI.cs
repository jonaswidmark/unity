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
    [SerializeField] private IClickable selectedObject;
    private ActionManager actionManager;
    private BaseAction baseAction;
    private StateMachine stateMachine;
    private MissionManager missionManager;
    [SerializeField] private MissionScriptableObject missionToStart;

    private void Start()
    {
        actionManager = ActionManager.Instance;
        stateMachine = StateMachine.Instance;
        missionManager= MissionManager.Instance;
        missionManager.OnNewMission += MissionManager_OnNewMission;
        button.interactable = true;
        button.gameObject.SetActive(false);
    }
    private void SetNewMission(MissionScriptableObject missionToStart)
    {
        
        button.gameObject.SetActive(true);
        button.interactable = true;
        button.onClick.AddListener(() => {
                missionManager.InitializeMission();
        });
        textMeshPro.text = missionToStart.Title;
    }

    private void MissionManager_OnNewMission(object sender, MissionEventArgs e)
    {
        missionToStart = e.Mission;
        SetNewMission(missionToStart);
    }
}
