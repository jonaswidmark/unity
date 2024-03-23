using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionManager : ServiceManager<ActionManager>
{
    public static event EventHandler OnAnySelected;
    public Action onActionComplete;
    private StateMachine stateMachine;
    private IClickable selectedTransform;
    private InputManager inputService;

    private string currentActionName;
    
    private void Start()
    {
        stateMachine = ServiceLocator.StateMachine;
        inputService = ServiceLocator.InputManager;
        inputService.OnMouseSelect += InputManager_OnSelect;
    }
    private void InputManager_OnSelect(object sender, EventArgs e)
    {
        /* if(WasOtherSelected())
        {
            RemoveVisual();
        }
        if(WasSelected())
        {
            SetVisual();
        } */
    }
    
    public string GetServiceName()
    {
        return "ActionManager";
    }
    public void SetSelectedAction(BaseAction baseAction)
    {
        if(stateMachine.GetPlayerState().GetType() ==  typeof(IdleState) )
        {
            GetDerivedClass(baseAction);
            IState missionState = new MissionState();
            stateMachine.ChangeState(missionState);
            //Debug.Log(stateMachine.GetPlayerState());
        }
        
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
        Debug.Log("GetDerivedClass");
        // This is where the action is triggered!
        currentActionName = obj.GetType().ToString();
        obj.TakeAction(onActionComplete);

        return currentActionName;
    }
    
}
