using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;
using System;

public class ServiceManager : MonoBehaviour
{
    public static ServiceManager Instance {get; private set;}
    private Dictionary<string, BaseService> registeredServices = new Dictionary<string, BaseService>();

    /* public static ServiceManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<ServiceManager>();
                if (instance == null)
                {
                    GameObject singleton = new GameObject("ServiceManager");
                    instance = singleton.AddComponent<ServiceManager>();
                }
            }
            return instance;
        }
    } */

    public void Awake()
    {
        if(Instance != null)
        {
            Debug.Log("There's more than one ServiceManager! " + transform + " - " + Instance);
            Destroy(gameObject);
            return ;
        }
        Instance = this;
        
           /*  if (Instance == null)
            {
                Instance = FindObjectOfType<ServiceManager>();
                if (Instance == null)
                {
                    GameObject singleton = new GameObject("ServiceManager");
                    Instance = singleton.AddComponent<ServiceManager>();
                }
            }
            Instance = this; */
        
    }
    private void Start()
    {
        
    }

    public void RegisterService(string serviceName, BaseService service)
    {
        if (!registeredServices.ContainsKey(serviceName))
        {
            registeredServices.Add(serviceName, service);
        }
        else
        {
            Debug.LogWarning("Service " + serviceName + " is already registered.");
        }
    }

    public BaseService GetService(string serviceName)
    {
        if (registeredServices.ContainsKey(serviceName))
        {
            return registeredServices[serviceName];
        }
        else
        {
            Debug.LogWarning("Service " + serviceName + " is not registered.");
            return null;
        }
    }
}
