using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Mission", menuName = "CustomObjects/Mission")]
public class MissionScriptableObject : ScriptableObject
{
    [SerializeField] private List<CountdownPurpose> missionTasks = new List<CountdownPurpose>();
    
    public List<CountdownPurpose> GetMissionTasks()
    {
        return missionTasks;
    }
}
