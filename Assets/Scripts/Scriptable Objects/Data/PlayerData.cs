using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "CustomObjects/Data")]
public class PlayerDataScriptableObject : ScriptableObject
{
    public Player playerObject; // Referens till spelarobjektet

    // Metod för att hämta komponenten från spelarobjektet
    public T GetPlayerComponent<T>() where T : Component
    {
        Debug.Log("GetPlayerComponent");
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
    public void SetPlayerObject(Player playerObject)
    {
        this.playerObject = playerObject;
    }
    public Player GetPlayerObject()
    {
        return playerObject;
    }
}