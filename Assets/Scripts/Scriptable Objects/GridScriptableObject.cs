using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Grid", menuName = "CustomObjects/Grid")]
public class GridScriptableObject : ScriptableObject
{
    private GridSystem gridSystem;
    private GridObject[,] gridObjectArray;
    private int width;
    private int height;
    private float cellSize;
    public void SetGridSystem(GridSystem gridSystem)
    {
        this.gridSystem = gridSystem;
        width = gridSystem.GetWidth();
        height = gridSystem.GetHeight();
        cellSize = gridSystem.GetCellSize();
        gridObjectArray = gridSystem.GetGridObjectArray();
    }
    public GridSystem GetGridSystem()
    {
        return gridSystem;
    }
    public void SetGridObjectArray(GridObject[,] gridObjectArray)
    {
        this.gridObjectArray = gridObjectArray;
    }
    public GridObject[,] GetGridObjectArray()
    {
        return gridObjectArray;
    }
    public void CreateLevelGrids(Transform debugPrefab)
    {
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                GridPosition gridPosition = new GridPosition(x, z);
                Transform debugTransform = GameObject.Instantiate(debugPrefab, GetWorldPosition(gridPosition), Quaternion.identity);
                GridDebugObject gridDebugObject = debugTransform.GetComponent<GridDebugObject>();
                
                gridDebugObject.SetGridObject(GetGridObject(gridPosition));
                //gridDebugObject.UpdateGridPositionText();
            }
        }
    }
    public Vector3 GetWorldPosition(GridPosition gridPosition)
    {
        return new Vector3(gridPosition.x, 0, gridPosition.z) * cellSize;
    }
    public GridPosition GetGridPosition(Vector3 worldPosition)
    {
        return new GridPosition(
            Mathf.RoundToInt(worldPosition.x / cellSize),
            Mathf.RoundToInt(worldPosition.z / cellSize)
        );
    }
    public GridObject GetGridObject(GridPosition gridPosition)
    {
        return gridObjectArray[gridPosition.x, gridPosition.z];
    }
}
