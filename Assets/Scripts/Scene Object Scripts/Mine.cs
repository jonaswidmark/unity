using System;
using UnityEngine;

public class Mine : BaseSceneObject
{
    public override void StartAddon()
    {
        foreach(MissionScriptableObject mission in missionScriptableObjectList)
        {
            mission.MissionTransform = transform;
        }
    }
    public override void InputManager_OnSelect(object sender, EventArgs e)
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
    public override bool WasSelected()
    { 
        Debug.Log(parentTransform);
        Debug.Log(transform);
        return Utils.WasSelected(this);
    }
    /* private void Start()
    {
        inputManager = ServiceLocator.InputManager;
        visualsManager = ServiceLocator.VisualsManager;
        inputManager.OnMouseSelect += InputManager_OnSelect;
        missionManager = ServiceLocator.MissionManager;
        missionManager.OnMissionEnded += MissionManager_OnMissionEnded;
        visualsManager.RemoveVisual(this);
        currentPrefab = Instantiate(currentPrefab, parentTransform);
    } */

    /* public override void InputManager_OnSelect(object sender, EventArgs e)
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
    } */
    /* public override bool WasSelected()
    { 
        return Utils.WasSelected(this);
    } */
  
}
