using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGrid : MonoBehaviour
{
    public static LevelGrid Instance {get; private set;}
    private GridSystem gridSystem;
    [SerializeField] private Transform gridObjectPrefab;
    [SerializeField] private GameObject gridObjectPrefabGO;
    [SerializeField] private GridScriptableObject gridScriptableObject;
    private void Awake()
    {
        if(Instance != null)
        {
            Debug.Log("There's more than one LevelGrid! " + transform + " - " + Instance);
            Destroy(gameObject);
            return ;
        }
        Instance = this;
        gridSystem = new GridSystem(50, 50, 2f);
        gridScriptableObject.SetGridSystem(gridSystem);
        gridScriptableObject.CreateLevelGrids(gridObjectPrefab);
    }
    public void PlaceTransformAtGridPosition(GridPosition gridPosition, Transform transform, Vector3 offsetPlacing, out Transform spawnedTransform)
    {
        SetTransformAtGridPosition(gridPosition, transform);
        SpawnTransformAtGridPosition(gridPosition, transform, offsetPlacing, out spawnedTransform);
    }
    public void SetTransformAtGridPosition(GridPosition gridPosition, Transform transform)
    {
        GridObject gridObject = gridScriptableObject.GetGridObject(gridPosition);
        gridObject.SetTranform(transform);
    }
    public void SpawnTransformAtGridPosition(GridPosition gridPosition, Transform transform, Vector3 offsetPlacing, out  Transform spawnedTransform)
    {
        spawnedTransform = null;
        GridObject gridObject = gridScriptableObject.GetGridObject(gridPosition);
        if(transform != null && gridObject != null)
        {
            Vector3 gridObjectWorldPosition = gridScriptableObject.GetWorldPosition(gridPosition);
            spawnedTransform = Instantiate(transform, gridObjectWorldPosition + offsetPlacing, Quaternion.identity);
        }
    }
    public Transform GetTransformAtGridPosition(GridPosition gridPosition)
    {
        return gridScriptableObject.GetGridObject(gridPosition).GetTranform();
    }
    public void ClearTransformAtGridPosition(GridPosition gridPosition)
    {
        gridScriptableObject.GetGridObject(gridPosition).SetTranform(null);
    }
}
