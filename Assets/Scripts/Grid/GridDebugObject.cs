using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GridDebugObject : MonoBehaviour
{
    private GridObject gridObject;
    [SerializeField] private TextMeshPro gridPositionTextMesh;
    
    public void SetGridObject(GridObject gridObject)
    {
        this.gridObject = gridObject;
    }
    public void UpdateGridPositionText()
    {
        //gridPositionTextMesh.SetText($"{gridPosition.x},{gridPosition.z}");
        gridPositionTextMesh.text = gridObject.ToString();
    }
}
