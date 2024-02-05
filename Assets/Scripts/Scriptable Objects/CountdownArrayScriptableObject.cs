using UnityEngine;

using System.Collections.Generic;

[CreateAssetMenu(fileName = "CountdownArray", menuName = "CustomObjects/CountdownArray")]
public class CountdownArrayScriptableObject : ScriptableObject
{
    private Dictionary<GameObject, CountdownScriptableObject> countdownStartedArray = new Dictionary<GameObject, CountdownScriptableObject>();
    private Dictionary<GameObject, CountdownScriptableObject> countdownCompletedArray = new Dictionary<GameObject, CountdownScriptableObject>();
    private Dictionary<GameObject, CountdownScriptableObject> countdownActiceArray = new Dictionary<GameObject, CountdownScriptableObject>();

    public Dictionary<GameObject, CountdownScriptableObject> GetCountdownStartedArray()
    {
        return countdownStartedArray;
    }
    public void AddToCountdownStartedArray(GameObject timerGameObject, CountdownScriptableObject timeSO)
    {
        countdownStartedArray.Add(timerGameObject, timeSO);
        UpdateCountdownActiveArray();
    }
    public Dictionary<GameObject, CountdownScriptableObject> GetCountdownCompletedArray()
    {
        return countdownCompletedArray;
    }
    public void AddToCountdownCompletedArray(GameObject timerGameObject, CountdownScriptableObject timeSO)
    {
        countdownCompletedArray.Add(timerGameObject, timeSO);
        UpdateCountdownActiveArray();
    }
    private void UpdateCountdownActiveArray()
    {
        countdownActiceArray.Clear();
        
        foreach (var kv in countdownStartedArray)
        {
            if (!countdownCompletedArray.ContainsKey(kv.Key))
            {
                countdownActiceArray.Add(kv.Key,kv.Value);
            }
        }
        
    }
    public Dictionary<GameObject, CountdownScriptableObject> GetCountdownActiveArray()
    {
        return countdownActiceArray;
    }

    
    
    
}
