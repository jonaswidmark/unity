using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BaseSceneObject :  MonoBehaviour, IClickable
{
    
    protected EventManager eventManager;
    protected VisualsManager visualsManager;
    protected MissionManager missionManager;
    protected MissionScriptableObject activeMission;
    [SerializeField] protected Transform currentPrefab;
    [SerializeField] protected Transform parentTransform;
    public float transitionDelay = 0.5f;
    protected MissionEventArgs missionEventArgs;
    [SerializeField] protected List<MissionScriptableObject> missionScriptableObjectList = new List<MissionScriptableObject>();
    private void Start()
    {
        eventManager = ServiceLocator.EventManager;
        visualsManager = ServiceLocator.VisualsManager;
        eventManager.OnMouseSelect += EventManager_OnSelect;
        missionManager = ServiceLocator.MissionManager;
        eventManager.OnMissionEnded += EventManager_OnMissionEnded;
        visualsManager.RemoveVisual(this);
        currentPrefab = Instantiate(currentPrefab, transform);
        StartAddon();
    }
    public virtual void StartAddon()
    {
        /** Used by derived classes **/
    }
    public virtual void EventManager_OnMissionEnded(object sender, MissionEventArgs e)
    {
        missionEventArgs = e;
        if(e.Mission.NewVisualTransform != null && e.Mission.MissionTransform == transform)
        {
            Destroy(currentPrefab.gameObject, transitionDelay);
            Invoke("SpawnNewObject", transitionDelay);
        }
    }
    public virtual void SpawnNewObject()
    {
        currentPrefab = Instantiate(missionEventArgs.Mission.NewVisualTransform, transform);
    }
    public virtual void EventManager_OnSelect(object sender, EventArgs e)
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
    public virtual bool WasSelected()
    { 
        return Utils.WasSelected(this);
    }
    public virtual void UpdateMissionList()
    {
        var nextMission = Utils.GetNextMission(missionScriptableObjectList);
        if (nextMission != null)
        {Debug.Log(nextMission);
            SetActiveMission(nextMission);
            missionManager.SetNewMissionAction(nextMission);
        }
        else
        {
            Debug.Log("Inga tillgängliga missioner hittades.");
        }
    }
    public virtual void SetActiveMission(MissionScriptableObject activeMission)
    {
        this.activeMission = activeMission;
    }
    public virtual Transform ObjectTransform
    {
        get
        {
            return transform;
        }
    }
    public virtual GameObject ObjectGameObject
    {
        get
        {
            return gameObject;
        }
    }
   
}
