using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridObject 
{
    private GridSystem gridSystem;
    private GridPosition gridPosition;
    private Transform transform;

    public GridObject(GridSystem gridSystem, GridPosition gridPosition)
    {
        this.gridSystem = gridSystem;
        this.gridPosition = gridPosition;
    }
    public override string ToString()
    {
        return gridPosition.ToString() + "\n" + transform;
    }
    public void SetTranform(Transform transform)
    {
        this.transform = transform;
    }
    public Transform GetTranform()
    {
        return transform;
    }
    

}
