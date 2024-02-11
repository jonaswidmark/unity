using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseAction : MonoBehaviour
{
    public static event EventHandler OnAnyActionStarted;
    public static event EventHandler OnAnyActionCompleted;
    protected Action onActionComplete;
    protected MissionManager missionManager;
    protected IClickable selectedObject;
    protected bool isActive;

    protected virtual void Awake()
    {
        selectedObject = GetComponent<IClickable>();
    }
    protected virtual void Start()
    {
        missionManager = MissionManager.Instance;
    }
    public IClickable GetSelectedObject()
    {
        return selectedObject;
    }
    public abstract string GetActionName();
    public abstract void TakeAction(Action onActionComplete);
    protected void ActionStart(Action onActionComplete)
    {
        isActive = true;
        this.onActionComplete = onActionComplete;
        OnAnyActionStarted?.Invoke(this, EventArgs.Empty);
    }
    protected void ActionComplete()
    {
        isActive = false;
        onActionComplete();
        OnAnyActionCompleted?.Invoke(this, EventArgs.Empty);
    }
    protected MissionManager GetMissionManager()
    {
        return missionManager;
    }
}
