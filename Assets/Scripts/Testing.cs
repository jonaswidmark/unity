using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    [SerializeField] private Transform gridObjectPrefab;
    void Start()
    {
        GridPosition testPosition = new GridPosition(7, 1);
        Vector3 offset = new Vector3(0,2,0);
        LevelGrid.Instance.PlaceTransformAtGridPosition(testPosition,gridObjectPrefab,offset);
    }

    private void Update()
    {
    
    }
}
