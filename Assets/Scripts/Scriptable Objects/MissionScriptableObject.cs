using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "Mission", menuName = "CustomObjects/Mission")]
public class MissionScriptableObject : ScriptableObject, ITask
{
    public List<ITask> missionTasks = new List<ITask>();
    [SerializeField] private string title;
    public bool isAvailable;

    public int missionOrder;
    
    public int MissionOrder
    {
        get { return missionOrder; }
        private set { missionOrder = value; }
    }
    public string Title
    {
        get { return title; }
        private set { title = value; }
    }
    public string GetTitle()
    {
        return Title;
    }
    new public int GetInstanceID()
    {
        return GetHashCode();
    }
    
    
    public List<ITask> GetMissionTasks()
    {
        Debug.Log("MissionScriptableObject");
        return missionTasks;
    }
    



}
