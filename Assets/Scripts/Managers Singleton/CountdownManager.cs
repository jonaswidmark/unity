using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class CountdownManager : MonoBehaviour
{
    public static CountdownManager Instance { get; private set; }
    public GameObject prefabToSpawn;
    public event EventHandler onMissionTaskComplete;
    [SerializeField] private CountdownArrayScriptableObject countdownArray;
    [SerializeField] private List<MissionTask> purposeList;
    [SerializeField] private Transform parentObject;

    private Dictionary<GameObject, CountdownScriptableObject> countdownDictionary = new Dictionary<GameObject, CountdownScriptableObject>();

    private Dictionary<GameObject, CountdownScriptableObject> countdownDictionaryCompleted = new Dictionary<GameObject, CountdownScriptableObject>();
    private void Awake()
    {
        if (Instance != null)
        {
            Debug.Log("There's more than one CountdownManager! " + transform + " - " + Instance);
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    public void SpawnPrefab(float initialTime, MissionTask missionTask, out GameObject spawnedPrefab, out CountdownScriptableObject countdownData)
    {
        spawnedPrefab = null;
        countdownData = null;
        if (prefabToSpawn != null && parentObject != null && countdownArray != null && missionTask != null)
        {
            spawnedPrefab = Instantiate(prefabToSpawn, parentObject);
            countdownData = CreateCountdownScriptableObject(initialTime, missionTask);
            countdownData.OnCountdownFinished += HandleCountdownFinished;
            countdownDictionary.Add(spawnedPrefab, countdownData);
            if (!countdownArray.GetCountdownStartedArray().ContainsKey(spawnedPrefab))
            {
                countdownArray.AddToCountdownStartedArray(spawnedPrefab, countdownData);
            }
        }
    }
    CountdownScriptableObject CreateCountdownScriptableObject(float initialTime, MissionTask missionTask)
    {
        CountdownScriptableObject countdownData = ScriptableObject.CreateInstance<CountdownScriptableObject>();
        countdownData.StartCountdown(initialTime);
        countdownData.SetCountdownMissionTask(missionTask);
        return countdownData;
    }
    void HandleCountdownFinished(CountdownScriptableObject countdownData)
    {
        var pairToRemove = countdownDictionary.FirstOrDefault(pair => pair.Value == countdownData);
        if (!pairToRemove.Key || countdownArray != null)
        {
            if (!countdownDictionaryCompleted.ContainsKey(pairToRemove.Key))
            {
                countdownDictionaryCompleted.Add(pairToRemove.Key, pairToRemove.Value);
            }
            if (!countdownArray.GetCountdownCompletedArray().ContainsKey(pairToRemove.Key))
            {
                countdownArray.AddToCountdownCompletedArray(pairToRemove.Key, pairToRemove.Value);
            }

        }
        string t = countdownData.GetCountdownMissionTask().GetKey();
        Debug.Log("Timer with task: " + t + " was completed!");
        Destroy(pairToRemove.Key);
        onMissionTaskComplete?.Invoke(this, EventArgs.Empty);
    }
    void Update()
    {
        if (countdownArray != null && countdownArray.GetCountdownActiveArray().Count > 0)
        {
            for (int i = 0; i < countdownDictionary.Count; i++)
            {
                var kvp = countdownDictionary.ElementAt(i);
                if (kvp.Key != null)
                {
                    GameObject spawnedPrefab = kvp.Key;
                    CountdownScriptableObject countdownData = kvp.Value;

                    countdownData.UpdateCountdown();
                    spawnedPrefab.GetComponent<Timer>().SetTimeText(DisplayTime(countdownData.GetTimeRemaining()));
                }
            }
        }
    }
    private string DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}

