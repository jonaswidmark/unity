using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class HomeBase :  MonoBehaviour, IClickable
{
    
    private CountdownPurpose purpose;
    private InputManager inputManager;
    private VisualsManager visualsManager;
    
    private void Start()
    {
        inputManager = InputManager.Instance;
        visualsManager = VisualsManager.Instance;
        inputManager.OnMouseSelect += InputManager_OnSelect;
        visualsManager.RemoveVisual(this);
    }
    private void InputManager_OnSelect(object sender, EventArgs e)
    {
        if(WasSelected())
        {
            visualsManager.SetVisual(this);
        }
        else 
        {
            visualsManager.RemoveVisual(this);
        }
    }
    protected bool WasSelected()
    { 
        return Utils.WasSelected(this);
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
