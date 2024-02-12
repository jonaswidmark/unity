using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    private IState currentState;
    [SerializeField] private Transform homeBasePrefab;
    private Transform homeBase;
    private void Awake()
    {
        if (Instance != null)
        {
            Debug.Log("There's more than one GameManager! " + transform + " - " + Instance);
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
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

    public void SetPlayerState(IState currentState)
    {
        this.currentState = currentState;
    }
    public IState GetPlayerState()
    {
        return currentState;
    }
}
