using UnityEngine;
using System.Linq;
using System.Collections.Generic;
public static class Utils
{
    private static IClickable selectedObject;

    private static readonly Dictionary<string, string> stringResources = new Dictionary<string, string>
    {
        { "PlayerIdleAnimation", "Breathing Idle" }, 
        { "PlayerSleepingAnimation", "Sleeping" },
        {"SleepIdle", "SleepIdle"}
    };
    public static bool WasSelected<T>(T obj) where T : MonoBehaviour
    {
        Ray ray = Camera.main.ScreenPointToRay(ServiceLocator.InputManager.GetMouseScreenPosition());
        bool wasHit = Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue) && raycastHit.collider.GetComponent<T>() != null;
        if (wasHit)
        {
            selectedObject = (IClickable)obj.GetComponent<T>();
            ServiceLocator.ActionManager.SetSelectedTransform((IClickable) obj);
        }
        return wasHit;
    }
    public static IClickable WhatClickableInterfaceSelected()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            IClickable clickableComponent = hit.collider.GetComponent<IClickable>();
            if (clickableComponent != null)
            {
                return clickableComponent;
            }
        }
        return null;
    }
    public static MissionScriptableObject GetNextMission(List<MissionScriptableObject> missionList)
    {
        var availableMissions = missionList.Where(mission => mission.isAvailable);
        var sortedMissions = availableMissions.OrderBy(mission => mission.missionOrder);
        return sortedMissions.FirstOrDefault();
    }
    public static string GetString(string key)
    {
        if (stringResources.ContainsKey(key))
        {
            return stringResources[key];
        }
        else
        {
            return "String not found";
        }
    }
}
