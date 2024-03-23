
using System;
using UnityEngine;

public class Village : BaseSceneObject
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
        Debug.Log("village: "+ Utils.WasSelected(this));
        return Utils.WasSelected(this);
    }
}
