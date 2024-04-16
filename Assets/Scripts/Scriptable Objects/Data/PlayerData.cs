using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "CustomObjects/Data")]
public class PlayerDataScriptableObject : ScriptableObject
{
    public GameObject playerObject; 
    private Player.PlayerStats playerStats;
    public T GetPlayerComponent<T>() where T : Component
    {
        if (playerObject != null)
        {
            return playerObject.GetComponent<T>();
        }
        else
        {
            Debug.LogError("Player object reference is null!");
            return null;
        }
    }
    public void SetPlayerStats(Player.PlayerStats playerStats)
    {
        this.playerStats = playerStats;
    }
    public Player.PlayerStats GetPlayerStats()
    {
        return playerStats;
    }
    public void SetPlayerObject(GameObject playerObject)
    {
        this.playerObject = playerObject;
    }
    public GameObject GetPlayerObject()
    {
        return playerObject;
    }
}