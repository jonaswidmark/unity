using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : BaseSceneObject
{
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
        Debug.Log("Mine: "+ Utils.WasSelected(this));
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
