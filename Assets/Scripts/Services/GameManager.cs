using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : ServiceManager<GameManager>
{
    private IState currentState;
    [SerializeField] private Transform homeBasePrefab;
    void Start()
    {
        //CreateHomeBase();
    }
    void CreateHomeBase()
    {
        GridPosition homeBasePosition = new GridPosition(2, 2);
        Vector3 offset = new Vector3(0,0.01f,0);
        Transform spawnedTransform;
        ServiceLocator.LevelGrid.PlaceTransformAtGridPosition(homeBasePosition,homeBasePrefab,offset, out spawnedTransform);
        spawnedTransform.rotation = Quaternion.Euler(90, 0, 0);
    }
    public void SetPlayerState(IState currentState)
    {
        this.currentState = currentState;
    }
    public IState GetPlayerState()
    {
        return currentState;
    }
}
