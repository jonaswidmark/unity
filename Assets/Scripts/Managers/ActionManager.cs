using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionManager : MonoBehaviour
{
    public static ActionManager Instance {get; private set;}
    public static event EventHandler OnAnySelected;
    private IClickable selectedTransform;
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
        //Debug.Log(baseAction);
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
    public void GetDerivedClass<T>(T obj) where T : BaseAction
    {
        // Gör något med obj...
        Debug.Log($"Anropad från: {obj.GetType().Name}");
    }
}
