using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "CountdownManagerSO", menuName = "ServicesSO/CountdownManagerSO")]
public class CountdownManagerSO : ScriptableObject
{
    public GameObject prefabToSpawn;
    [SerializeField] EventArgsSO OnCountDownOrCallbackComplete;
    [SerializeField] private CountdownArrayScriptableObject countdownArray;
    [SerializeField] private List<MissionTask> purposeList;
    private Transform parentObject;
    private MissionTask missionTask;
    private GameObject missionTaskPrefab;
    private CountdownScriptableObject countdownScriptableObject;
    private Dictionary<GameObject, CountdownScriptableObject> countdownDictionary = new Dictionary<GameObject, CountdownScriptableObject>();
    private Dictionary<GameObject, CountdownScriptableObject> countdownDictionaryCompleted = new Dictionary<GameObject, CountdownScriptableObject>();
    private Vector3 newTransformPosition = new Vector3(0,0,0);

    public void SetParentObject(Transform parentObject)
    {
        this.parentObject = parentObject;
    }
    private void UpdateActiveComponents(){
        if(missionTask == null)
        {
            return ;
        }
        if (countdownArray != null && countdownArray.GetCountdownActiveArray().Count > 0)
            {
                for (int i = 0; i < countdownDictionary.Count; i++)
                {
                    var kvp = countdownDictionary.ElementAt(i);
                    if (kvp.Key != null)
                    {
                        missionTaskPrefab = kvp.Key;
                        countdownScriptableObject = kvp.Value;
                    }
                }
            }
        if(missionTask.GetIsCompletedBy() == MissionTask.IsCompletedBy.callback && missionTask.showText)
        {
            missionTaskPrefab.GetComponent<Timer>().SetTimeText(GetTextToDisplay(missionTask), missionTask.typeText);
        }
    }
    private string GetTextToDisplay(MissionTask missionTask)
    {
        string textToDisplay = missionTask.Title;
            
        if(!string.IsNullOrEmpty(missionTask.TextToDisplay))
        {
            textToDisplay = missionTask.TextToDisplay;
        }
        return textToDisplay;
    }
    public void SpawnPrefab(float initialTime, MissionTask missionTask, out GameObject spawnedPrefab, out CountdownScriptableObject countdownData, Vector2 countdownsPlacing = default)
    {
        if (countdownsPlacing != Vector2.zero)
        {
            float newXPlacingOfCountdown = countdownsPlacing.x;
            float newYPlacingOfCountdown = countdownsPlacing.y;
            newTransformPosition = new Vector3(newXPlacingOfCountdown, newYPlacingOfCountdown, 0);
        } 
       
        spawnedPrefab = null;
        countdownData = null;
        this.missionTask = missionTask;
        if (prefabToSpawn != null && parentObject != null && countdownArray != null && missionTask != null)
        {
            spawnedPrefab = Instantiate(prefabToSpawn, parentObject);
            spawnedPrefab.transform.localPosition = newTransformPosition;
            countdownData = CreateCountdownScriptableObject(initialTime, missionTask);
            countdownData.OnCountdownFinished += HandleCountdownFinished;
            countdownDictionary.Add(spawnedPrefab, countdownData);
            missionTask.SetActiveCountdown(countdownData);
            if (!countdownArray.GetCountdownStartedArray().ContainsKey(spawnedPrefab))
            {
                countdownArray.AddToCountdownStartedArray(spawnedPrefab, countdownData);
            }
        }
        UpdateActiveComponents();
    }
    CountdownScriptableObject CreateCountdownScriptableObject(float initialTime, MissionTask missionTask)
    {
        CountdownScriptableObject countdownData = ScriptableObject.CreateInstance<CountdownScriptableObject>();
        countdownData.SetCountdownMissionTask(missionTask);
        if(missionTask.GetIsCompletedBy() == MissionTask.IsCompletedBy.predefinedTimer)
        {
            countdownData.StartCountdown(initialTime);
        }
        
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
        //string t = countdownData.GetCountdownMissionTask().GetKey();
        Destroy(pairToRemove.Key);
        UpdateActiveComponents();
        OnCountDownOrCallbackComplete.RaiseEvent(EventArgs.Empty);
    }
    public void UpdateAction()
    {
        if(missionTaskPrefab == null || countdownScriptableObject == null)
        {
            return;
        }
        if(missionTask.GetIsCompletedBy() == MissionTask.IsCompletedBy.predefinedTimer)
        {
            if (countdownArray != null && countdownArray.GetCountdownActiveArray().Count > 0)
            {
                for (int i = 0; i < countdownDictionary.Count; i++)
                {
                    var kvp = countdownDictionary.ElementAt(i);
                    if (kvp.Key != null)
                    {
                        countdownScriptableObject.UpdateCountdown();
                        string stringToDisplay = "";
                        
                        if (missionTask.ShowText)
                        {   
                            stringToDisplay = GetTextToDisplay(missionTask) + "\n";
                        }
                        if (missionTask.ShowTimer)
                        {
                            stringToDisplay += DisplayTime(countdownScriptableObject.GetTimeRemaining());
                        }
                        
                        missionTaskPrefab.GetComponent<Timer>().SetTimeText(stringToDisplay, missionTask.typeText);
                        
                    }
                }
            }
        }
        else
        {
            if(missionTask.showText)
            {
                missionTaskPrefab.GetComponent<Timer>().SetTimeText(GetTextToDisplay(missionTask), missionTask.typeText);
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

