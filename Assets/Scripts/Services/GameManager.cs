using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform homeBasePrefab;
    private Transform homeBase;
    void Start()
    {
        CreateHomeBase();
        
    }
    void CreateHomeBase()
    {
        GridPosition homeBasePosition = new GridPosition(2, 2);
        Vector3 offset = new Vector3(0,0.01f,0);
        Transform spawnedTransform;
        LevelGrid.Instance.PlaceTransformAtGridPosition(homeBasePosition,homeBasePrefab,offset, out spawnedTransform);
        spawnedTransform.rotation = Quaternion.Euler(90, 0, 0);
    }

    private void Update()
    {
    
    }
}
