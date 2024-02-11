using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionService : MonoBehaviour, IService
{
   
    public static event EventHandler OnAnySelected;
    public Action onActionComplete;
    private MissionService missionService;
    private IClickable selectedTransform;
    
    private string currentActionName;
    
    private void Start()
    {
        MissionService missionService = ServiceLocator.GetService<MissionService>("MissionService");

        
    }
    public string GetServiceName()
    {
        return "ActionService";
    }
    
    public void SetSelectedAction(BaseAction baseAction)
    {
        GetDerivedClass(baseAction);
    }
    
    public void SetSelectedTransform(IClickable selectedTransform)
    {
        this.selectedTransform = selectedTransform;
        OnAnySelected?.Invoke(this, EventArgs.Empty);
    }
    public IClickable GetSelectedTransform()
    {
        return selectedTransform;
    }
    public string GetDerivedClass<T>(T obj) where T : BaseAction
    {
        // This is where the action is triggered!
        currentActionName = obj.GetType().ToString();
        obj.TakeAction(onActionComplete);
        
        return currentActionName;
    }
}
