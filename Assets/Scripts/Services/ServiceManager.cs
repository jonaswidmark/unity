using UnityEngine;
using System.Collections.Generic;

public class ServiceManager : MonoBehaviour
{
    public static ServiceManager Instance { get; private set; }
    private Dictionary<string, IService> registeredServices = new Dictionary<string, IService>();

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.Log("There's more than one ServiceManager! " + transform + " - " + Instance);
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void RegisterService(string serviceName, IService service)
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

    public IService GetService(string serviceName)
    {
        Debug.Log(registeredServices.Count);
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
