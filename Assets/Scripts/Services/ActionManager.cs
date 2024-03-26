using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionManager : ServiceManager<ActionManager>
{
    public static event EventHandler OnAnySelected;
    public Action onActionComplete;
    private IClickable selectedTransform;
    

    private string currentActionName;
    
    
    
    public string GetServiceName()
    {
        return "ActionManager";
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
    /* public string GetDerivedClass<T>(T obj) where T : BaseAction
    {
        Debug.Log("GetDerivedClass");
        // This is where the action is triggered!
        currentActionName = obj.GetType().ToString();
        obj.TakeAction(onActionComplete);

        return currentActionName;
    } */
    
}
