using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildAction : BaseAction
{
    private string currentVerb = "Build";
    private string currentNoun = "Shelter";
    public override string GetActionName()
    {
        return currentVerb + " " + currentNoun;;
    }
    public override void TakeAction(Action onActionComplete)
    {
        Debug.Log("Building!");
        ActionStart(onActionComplete);
        if(GetMissionManager() != null)
        {
            GetMissionManager().InitializeMission();
        }
        
    }
    public string GetCurrentMission()
    {
        return currentVerb + " " + currentNoun;
    }
    public void SetCurrenctVerb(string currentVerb)
    {
        this.currentVerb = currentVerb;
    }
    public void SetCurrenctNoun(string currentNoun)
    {
        this.currentNoun = currentNoun;
    }
}
