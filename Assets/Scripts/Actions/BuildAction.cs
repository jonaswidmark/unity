using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildAction : BaseAction
{
    public override string GetActionName()
    {
        return "Build";
    }
    public override void TakeAction(Action onActionComplete)
    {
        Debug.Log("Building!");
        ActionStart(onActionComplete);
        missionManager.InitializeMission();
    }
}
