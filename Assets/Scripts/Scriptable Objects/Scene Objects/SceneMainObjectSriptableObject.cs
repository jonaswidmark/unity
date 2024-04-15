using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "SceneAsset", menuName = "SceneAssets/MainObject")]
public class SceneMainObjectScriptableObject : ScriptableObject
{
    [SerializeField] Vector3 prefabTransformPosition;
    [SerializeField] GameObject thisGameObject;
    private GameObject spawnedGameObject;
    public void SpawnPrefab(GameObject prefab)
    {
        if (prefab != null)
        {
            GameObject spawnedObject = Instantiate(prefab);
            spawnedObject.transform.position += prefabTransformPosition; 
            spawnedGameObject = spawnedObject;
        }
        else
        {
            Debug.LogWarning("Prefab is not set.");
        }
    }
    public GameObject GetThisGameObject()
    {
        return thisGameObject;
    }
    public GameObject GetSpawnedGameObject()
    {
        return spawnedGameObject;
    }
}