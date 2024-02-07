using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionManager : MonoBehaviour
{
    public static MissionManager Instance { get; private set; }
    private CountdownManager countdownManager;
    [SerializeField] private Transform parentObject;
    private MissionScriptableObject mission;
    [SerializeField] private CountdownPurpose purpose;
    private float purposeTimer = 9f;
    private void Awake()
    {
        if (Instance != null)
        {
            Debug.Log("There's more than one MonoBehaviour! " + transform + " - " + Instance);
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    private void Start()
    {
        countdownManager = CountdownManager.Instance;
    }

    public void InitializeMission()//MissionScriptableObject mission)
    {
        countdownManager.SpawnPrefab(purposeTimer, purpose);
    }

}
