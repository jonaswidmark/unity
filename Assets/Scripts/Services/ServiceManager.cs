using UnityEngine;
using System.Collections.Generic;
using System;
public class ServiceManager : MonoBehaviour
{
    public static ServiceManager Instance { get; private set; }
    //private Dictionary<string, Type> registeredServices = new Dictionary<string, Type>();
    private Dictionary<string, IService> registeredServices = new Dictionary<string, IService>();
    private Dictionary<string, Type> serviceTypes = new Dictionary<string, Type>();

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

    public void RegisterService<T>(string serviceName, T serviceInstance) where T : IService
{
    Debug.Log("RegisterService i Service Manager");
    if (!registeredServices.ContainsKey(serviceName))
    {
        registeredServices.Add(serviceName, serviceInstance);
        serviceTypes.Add(serviceName, typeof(T));
    }
    else
    {
        Debug.LogWarning("Service " + serviceName + " is already registered.");
    }
}

    public T GetService<T>(string serviceName) where T : class, IService
{
    
    if (registeredServices.ContainsKey(serviceName))
    {
        
        Type serviceType = registeredServices[serviceName].GetType(); // Hämta typen av tjänsten
        Type serviceTypeX = serviceTypes[serviceName];
        
        if (serviceType == typeof(T))
        {
            return Activator.CreateInstance(serviceType) as T;
        }
        else
        {
            Debug.LogWarning($"Service '{serviceName}' is not of type {typeof(T).Name}");
            return null;
        }
    }
    else
    {
        Debug.LogWarning("Service " + serviceName + " is not registered.");
        return null;
    }
}
}
