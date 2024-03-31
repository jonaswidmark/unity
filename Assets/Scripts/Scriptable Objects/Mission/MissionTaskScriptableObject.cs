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
    [SerializeField] string playAnimation;
    private CountdownScriptableObject activeCountdown;
    [SerializeField] string showText;
    public enum IsCompletedBy
    {
        predefinedTimer,
        callback
    }
    [SerializeField] IsCompletedBy isCompletedBy = IsCompletedBy.predefinedTimer;
    
    public string GetPlayAnimation()
    {
        return playAnimation;
    }
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
    public string ShowText
    {
        get { return showText; }
        private set { showText = value; }
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
