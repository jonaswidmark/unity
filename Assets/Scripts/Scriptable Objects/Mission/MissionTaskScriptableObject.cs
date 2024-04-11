using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New MissionTask", menuName = "CustomObjects/MissionTask")]
public class MissionTask : ScriptableObject
{
    public enum IsCompletedBy
    {
        predefinedTimer,
        callback
    }
    public enum TransformAction 
    {
        playerGoTo, alertArrow
    }
    [SerializeField] IsCompletedBy isCompletedBy = IsCompletedBy.predefinedTimer;
    [SerializeField] int id;
    [SerializeField] string key;
    [SerializeField] string title;
    [Header("Title of target Transform")]
    [SerializeField] string toTransformTitle;
    [SerializeField] TransformAction targetTransformAction = TransformAction.playerGoTo;
    [Header("Timer options")]
    [SerializeField] float timeToExecute;
    [SerializeField] bool showTimer;
    [Header("Title of optional animation in Animator")]
    [SerializeField] string playAnimation;
    [SerializeField] float timeToCrossFade;
    [SerializeField] bool useTimeToCrossFade = false;
    private CountdownScriptableObject activeCountdown;
    [Header("Text to display")]
    [TextArea]
    [SerializeField] string textToDisplay;
    public bool showText;
    public bool typeText;
    [Header("X and Y for local placing offset \nfrom default upper left corner")]
    [SerializeField] Vector2 placing;
    [Header("Camera placement")]
    [SerializeField] Vector3 cameraPosition;
    [SerializeField] Vector3 cameraLocalPosition;
    [SerializeField] Quaternion cameraRotation;
    
    
    public string GetPlayAnimation()
    {
        return playAnimation;
    }
    public IsCompletedBy CompletedBy { get; private set; }
    public TransformAction TargetTransformAction { get; private set; }
    public IsCompletedBy GetIsCompletedBy()
    {
        return isCompletedBy;
    }
    public TransformAction GetTransformAction()
    {
        return targetTransformAction;
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
    public bool TypeText
    {
        get { return typeText; }
        private set { typeText = value; }
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
    public Vector3 CameraPosition
    {
        get { return cameraPosition; }
        private set { cameraPosition = value; }
    }
    public Vector3 CameraLocalPosition
    {
        get { return cameraLocalPosition; }
        private set { cameraLocalPosition = value; }
    }
    public Quaternion CameraRotation
    {
        get { return cameraRotation; }
        private set { cameraRotation = value; }
    }
    public Transform GetToTransform()
    {
        GameObject obj = GameObject.Find(toTransformTitle);
        return obj != null ? obj.transform : null;
    }
    public float TimeToCrossFade
    {
        get { return timeToCrossFade;}
        private set 
        { timeToCrossFade = value;}
    }
    public bool UseTimeToCrossFade
    {
        get { return useTimeToCrossFade;}
        set { useTimeToCrossFade = value;}
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
