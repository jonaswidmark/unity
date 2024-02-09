using UnityEngine;
using System.Collections.Generic;

public class ServiceManager : MonoBehaviour
{
    private static ServiceManager instance;
    private Dictionary<string, BaseService> registeredServices = new Dictionary<string, BaseService>();

    public static ServiceManager Instance
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
