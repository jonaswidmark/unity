using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour, IService
{
    [SerializeField] private Transform gridObjectPrefab;

    void Start()
    {
        // Här kan du ha eventuella startåtgärder
    }

    public string GetServiceName()
    {
        return "Testing";
    }
}
