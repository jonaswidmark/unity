using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionManager : MonoBehaviour
{
    public static ActionManager Instance {get; private set;}
    public static event EventHandler OnAnySelected;
    private IClickable selectedTransform;
    
    private string currentActionName;
    public void Awake()
    {
        if(Instance != null)
        {
            Debug.Log("There's more than one ActionManager! " + transform + " - " + Instance);
            Destroy(gameObject);
            return ;
        }
        Instance = this;
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
        currentActionName = obj.GetType().ToString();
        return currentActionName;
    }
}
