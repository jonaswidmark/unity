using System;
using UnityEngine;

[CreateAssetMenu(fileName = "SceneAsset", menuName = "SceneAssets/MainObject")]
public class SceneMainObjectScriptableObject : ScriptableObject
{
    [SerializeField] Vector3 prefabTransformPosition;
    [SerializeField] GameObject thisGameObject;
    [SerializeField] int ifPlayerIndex;
    private GameObject spawnedGameObject;
    private GameManagerSO gameManager;
    private void OnEnable()
    {
        ServiceLocatorSO.InitializeManagers();
        gameManager = ServiceLocatorSO.GameManagerSO;
        gameManager.OnStartGame += GameManager_OnStartGame;
    }
    private void GameManager_OnStartGame(object sender,EventArgs e)
    {
        
    }
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
    public IClickable GetSpawnedObjectAsClickableInterface()
    {
        BaseSceneObject baseSceneObject = spawnedGameObject.GetComponent<BaseSceneObject>();
        IClickable iClickable = baseSceneObject;
        return iClickable;
    }
    public GameObject GetSpawnedGameObject()
    {
        return spawnedGameObject;
    }
    public int GetIfPlayerIndex()
    {
        return ifPlayerIndex;
    }
}