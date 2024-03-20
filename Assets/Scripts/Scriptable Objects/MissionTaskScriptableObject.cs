using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New MissionTask", menuName = "CustomObjects/MissionTask")]
public class MissionTask : ScriptableObject
{
    [SerializeField] int id;
    [SerializeField] string key;
    [SerializeField] string title;
    [SerializeField] string toTransformTitle;
    [SerializeField] float timeToExecute;
    private CountdownScriptableObject activeCountdown;
    public enum IsCompletedBy
    {
        predefinedTimer,
        callback
    }
    [SerializeField] IsCompletedBy isCompletedBy = IsCompletedBy.predefinedTimer;
    
    public IsCompletedBy CompletedBy { get; private set; }
    
    public IsCompletedBy GetIsCompletedBy()
    {
        return isCompletedBy;
    }
    public int Id
    {
        get { return id; }
        private set { id = value; }
    }
    public string Key
    {
        get { return key; }
        private set { key = value; }
    }
    public string Title
    {
        get { return title; }
        private set { title = value; }
    }
    public Transform GetToTransform()
    {
        GameObject obj = GameObject.Find(toTransformTitle);
        return obj != null ? obj.transform : null;
    }
    public int GetId()
    {
        return id;
    }
    public string GetKey()
    {
        return key;
    }
    public string GetTitle()
    {
        return title;
    }
    public float TimeToExecute
    {
        get { return timeToExecute; }
        set { timeToExecute = value; }
    }
    public void SetActiveCountdown(CountdownScriptableObject activeCountdown)
    {
        this.activeCountdown = activeCountdown;
    }
    public CountdownScriptableObject GetActiveCountdown()
    {
        return activeCountdown;
    }
    
}
