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
    [SerializeField] private IClickable selectedObject;
    private ActionManager actionManager;
    private BaseAction baseAction;
    private StateMachine stateMachine;
    private MissionManager missionManager;

    private void Start()
    {
        actionManager = ActionManager.Instance;
        stateMachine = StateMachine.Instance;
        missionManager= MissionManager.Instance;
        stateMachine.OnMissionState += StateMachine_OnMissionState;
        stateMachine.OnIdleState += StateMachine_OnIdleState;
        missionManager.OnUpdatedMissionList += MissionManager_OnUpdatedMissionList;
        button.interactable = true;
    }

    private void MissionManager_OnUpdatedMissionList(object sender, MissionEventArgs e)
    {
        Debug.Log("Knappen!");
        Debug.Log(e.Title);
    }

    private void ToggleSelectable()
    {
        button.interactable = !button.interactable;
    }
    
    private void StateMachine_OnMissionState(object sender, EventArgs e)
    {
        ToggleSelectable();
    }
    private void StateMachine_OnIdleState(object sender, EventArgs e)
    {
        ToggleSelectable();
    }
    public void SetBaseAction(BaseAction baseAction)
    {
        this.baseAction = baseAction;
        textMeshPro.text = baseAction.GetActionName().ToUpper();
        Debug.Log(textMeshPro.text);
        button.onClick.AddListener(() => {
                actionManager.SetSelectedAction(baseAction);
        });
    } 
    public void UpdateSelectedVisual()
    {
        /*  BaseAction selectedBaseAction = actionManager.GetSelectedAction();
        selectedBaseAction.SetActive(selectedBaseAction == baseAction);  */
    }
}
