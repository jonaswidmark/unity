using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New MissionTask", menuName = "CustomObjects/MissionTask")]
public class MissionTask : ScriptableObject
{
    [Header("Basic parameters")]
    [SerializeField] int id;
    [SerializeField] string key;
    [SerializeField] string title;
    [Header("Title of target Transform")]
    [SerializeField] string toTransformTitle;
    [Header("Timer options")]
    [SerializeField] float timeToExecute;
    [SerializeField] bool showTimer;
    [Header("Title of optional animation in Animator")]
    [SerializeField] string playAnimation;
    private CountdownScriptableObject activeCountdown;
    [Header("Text to display")]
    [SerializeField] string textToDisplay;
    [SerializeField] bool showText;
    [Header("X and Y for local placing offset \nfrom default upper left corner")]
    [SerializeField] Vector2 placing;
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
    public string TextToDisplay
    {
        get { return textToDisplay; }
        private set { textToDisplay = value; }
    }
    public bool ShowText
    {
        get { return showText; }
        private set { showText = value; }
    }
    public bool ShowTimer
    {
        get { return showTimer; }
        private set { showTimer = value; }
    }
    public Vector2 Placing
    {
        get { return placing; }
        private set { placing = value; }
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
