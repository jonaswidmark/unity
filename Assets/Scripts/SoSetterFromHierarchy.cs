using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoSetterFromHierarchy : MonoBehaviour
{
    [SerializeField] CountdownManagerSO countdownManagerSO;
    [SerializeField] EventManagerSO eventManagerSO;
    [SerializeField] GameManagerSO gameManagerSO;
    void Awake()
    {
        gameManagerSO.AwakeAction();
    }
    void Start()
    {
        /** Countdown Manager SO **/
        ServiceLocatorSO.InitializeManagers();
        GameObject countdownsObject = GameObject.Find("Countdowns");
        if (countdownsObject != null)
        {
            Transform countdownsTransform = countdownsObject.transform;
            countdownManagerSO.SetParentObject(countdownsTransform);
        }
        else
        {
            Debug.LogWarning("Objekt med namnet 'Countdowns' hittades inte.");
        }

        /** Event Manager SO **/
        eventManagerSO.StartAction();

        
    }
    void Update()
    {
        countdownManagerSO.UpdateAction();
    }
  
}
