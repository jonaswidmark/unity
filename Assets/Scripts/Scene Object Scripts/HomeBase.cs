using System;
using UnityEngine;

public class HomeBase :  BaseSceneObject, IClickable
{
    public override void StartAddon()
    {
        Transform alertArrowTransform = transform.Find("AlertArrow");
        alertArrow = alertArrowTransform.GetComponent<AlertArrow>();
        //alertArrow.ToggleArrow();
        foreach(MissionScriptableObject mission in missionScriptableObjectList)
        {
            mission.MissionTransform = transform;
        }
    }
    public override void EventManager_OnSelect(object sender, EventArgs e)
    {
        if(!isSelectable)
        {
            return;
        }
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
        return Utils.WasSelected(this);
    }
    public override void EventManager_OnToggleAlertArrow(object sender, MissionTaskEventArgs e)
    {
        Transform transfomForArrow = e.missionTask.GetToTransform();
        if(transfomForArrow == transform)
        {
            ToggleAllertArrow();
        }
    }
}
