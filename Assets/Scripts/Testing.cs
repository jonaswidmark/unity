using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : BaseService
{
    [SerializeField] private Transform gridObjectPrefab;

    //private ServiceManager serviceManager;
    void Start()
    {
        /* GridPosition testPosition = new GridPosition(7, 1);
        Vector3 offset = new Vector3(0,2,0);
        LevelGrid.Instance.PlaceTransformAtGridPosition(testPosition,gridObjectPrefab,offset); */
        //serviceManager = ServiceManager.Instance;
        //Debug.Log(serviceManager);
    }

    public override string GetServiceName()
    {
        Debug.Log("public override string GetServiceName() from Testing");
        return "Testing";
    }
    
}
