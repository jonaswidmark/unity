using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Mission", menuName = "CustomObjects/Mission")]
public class MissionScriptableObject : ScriptableObject
{
    [SerializeField] private List<CountdownPurpose> missionTasks = new List<CountdownPurpose>();
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
    
    
    public List<CountdownPurpose> GetMissionTasks()
    {
        Debug.Log("MissionScriptableObject");
        return missionTasks;
    }
}
