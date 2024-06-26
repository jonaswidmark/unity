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
        none, playerGoTo, alertArrow
    }
    [SerializeField] IsCompletedBy isCompletedBy = IsCompletedBy.predefinedTimer;
    [SerializeField] int id;
    [SerializeField] string key;
    [SerializeField] string title;
    [Header("Title of target Transform")]
    [Tooltip("This can be the target for the palyer movement or other, such as alert arrow")]
    [SerializeField] Transform toTransform;
    [SerializeField] SceneMainObjectScriptableObject toTransformSO;
    [SerializeField] TransformAction targetTransformAction = TransformAction.none;
    [Header("Timer options")]
    [Tooltip("Seconds for the task to complete, shown as a count down if the show timer box is ticked")]
    [SerializeField] float timeToExecute;
    [SerializeField] bool showTimer;
    [Header("Optional animation")]
    [Tooltip("Chose an animation from the animator controller to play during the task")]
    [SerializeField] string playAnimation;
    [Tooltip("Seconds to fade from previous animation to the new animation")]
    [SerializeField] float timeToCrossFade;
    [Tooltip("If unticked, the animation will occur emediately")]
    [SerializeField] bool useTimeToCrossFade = false;
    private CountdownScriptableObject activeCountdown;
    [Tooltip("Text to display, if the show text box is ticked")]
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
        return toTransform != null ? toTransform : null;
    }
    public SceneMainObjectScriptableObject GetToTransformSO()
    {
        return toTransformSO;
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
