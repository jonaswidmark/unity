using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionManager : MonoBehaviour
{
    public static ActionManager Instance { get; private set; }
    public static event EventHandler OnAnySelected;
    public Action onActionComplete;
    private GameManager gameManager;
    private StateMachine stateMachine;
    private IClickable selectedTransform;

    private string currentActionName;
    private void Awake()
    {
        if (Instance != null)
        {
            Debug.Log("There's more than one ActionManager! " + transform + " - " + Instance);
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    private void Start()
    {
        gameManager = GameManager.Instance;
        stateMachine = StateMachine.Instance;
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
    public void SetStartMission(MissionScriptableObject newMission)
    {
        Debug.Log("starting " + newMission);
    }
}
