using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "Mission", menuName = "CustomObjects/Mission")]
public class MissionScriptableObject : ScriptableObject, ITask
{
    public List<ScriptableObject> missionTasks = new List<ScriptableObject>();
    [SerializeField] private string title;
    [SerializeField] Transform previousVisualTransform;
    [SerializeField] Transform newVisualTransform;
    public bool isAvailable;
    public int missionOrder;
    
    public Transform PreviousVisualTransform
    {
        get { return previousVisualTransform; }
        set { previousVisualTransform = value; }
    }
    public Transform NewVisualTransform
    {
        get { return newVisualTransform; }
        set { newVisualTransform = value; }
    }
    
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
    
    
    public List<ScriptableObject> GetMissionTasks()
    {
        Debug.Log("MissionScriptableObject");
        return missionTasks;
    }
    



}
