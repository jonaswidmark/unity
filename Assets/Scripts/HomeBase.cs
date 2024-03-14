using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HomeBase :  MonoBehaviour, IClickable
{
    
    private InputManager inputManager;
    private VisualsManager visualsManager;
    private MissionManager missionManager;
    private MissionScriptableObject activeMission;
    
    [SerializeField] private List<MissionScriptableObject> missionScriptableObjectList = new List<MissionScriptableObject>();
    private void Start()
    {
        inputManager = InputManager.Instance;
        visualsManager = VisualsManager.Instance;
        inputManager.OnMouseSelect += InputManager_OnSelect;
        missionManager = MissionManager.Instance;
        visualsManager.RemoveVisual(this);
    }
    private void InputManager_OnSelect(object sender, EventArgs e)
    {
        if(WasSelected())
        {
            visualsManager.SetVisual(this);
            UpdateMissionList();
        }
        else if(Utils.WhatClickableInterfaceSelected() != null)
        {
            visualsManager.RemoveVisual(this);
        }
    }
    protected bool WasSelected()
    { 
        return Utils.WasSelected(this);
    }
    public void UpdateMissionList()
    {
        var nextMission = Utils.GetNextMission(missionScriptableObjectList);
        if (nextMission != null)
        {
            SetActiveMission(nextMission);
            missionManager.SetNewMissionAction(nextMission);
        }
        else
        {
            Debug.Log("Inga tillgängliga missioner hittades.");
        }
    }
    private void SetActiveMission(MissionScriptableObject activeMission)
    {
        this.activeMission = activeMission;
    }
    public Transform ObjectTransform
    {
        get
        {
            return transform;
        }
    }
    public GameObject ObjectGameObject
    {
        get
        {
            return gameObject;
        }
    }
   
}
