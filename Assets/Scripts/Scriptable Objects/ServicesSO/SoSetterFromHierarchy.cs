using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoSetterFromHierarchy : MonoBehaviour
{
    [Header("Objects for Countdown Manager")]
    [SerializeField] CountdownManagerSO countdownManagerSO;
    [SerializeField] Transform countdownsTransform;
    [Header("Objects for Event Manager")]
    [SerializeField] EventManagerSO eventManagerSO;
    [Header("Objects for Game Manager")]
    [SerializeField] GameManagerSO gameManagerSO;
    [Header("Objects for Input Manager")]
    [SerializeField] InputManagerSO inputManagerSO;
    [Header("Objects for Camera Manager")]
    [SerializeField] CameraManagerSO cameraManagerSO;
    [SerializeField] Transform cameraRig;
    [SerializeField] Transform mainCamera;
    void Awake()
    {
        gameManagerSO.AwakeAction();
        inputManagerSO.AwakeAction();
    }
    void Start()
    {
        /** Countdown Manager SO **/
        ServiceLocatorSO.InitializeManagers();
        countdownManagerSO.SetParentObject(countdownsTransform);
        /** Event Manager SO **/
        eventManagerSO.StartAction();
        /** Camera Manager SO **/
        cameraManagerSO.SetCameraRig(cameraRig);
        cameraManagerSO.SetMainCamera(mainCamera);
        cameraManagerSO.StartAction();
    }
    void Update()
    {
        countdownManagerSO.UpdateAction();
        cameraManagerSO.UpdateAction();
    }
}
