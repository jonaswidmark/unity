using UnityEngine;
using System.Linq;
using System.Collections.Generic;
public static class Utils
{
    private static IClickable selectedObject;
    
    public static bool WasSelected<T>(T obj) where T : MonoBehaviour
    {
        Ray ray = Camera.main.ScreenPointToRay(InputManager.Instance.GetMouseScreenPosition());
        bool wasHit = Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue) && raycastHit.collider.GetComponent<T>() != null;
        if (wasHit)
        {
            selectedObject = (IClickable)obj.GetComponent<T>();
            ActionManager.Instance.SetSelectedTransform((IClickable) obj);
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
}