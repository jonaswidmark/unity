using UnityEngine;
using System;
using System.Collections.Generic;

public class ServiceManager : MonoBehaviour
{
    public static ServiceManager Instance { get; private set; }

    private Dictionary<Type, IService> registeredServices = new Dictionary<Type, IService>();

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogWarning("There's more than one ServiceManager instance!");
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void RegisterService<T>(T serviceInstance) where T : IService
    {
        Type serviceType = typeof(T);
        if (!registeredServices.ContainsKey(serviceType))
        {
            registeredServices.Add(serviceType, serviceInstance);
        }
        else
        {
            Debug.LogWarning($"Service {serviceType.Name} is already registered.");
        }
    }

    public T GetService<T>(string v) where T : class, IService
    {
        Type serviceType = typeof(T);
        if (registeredServices.ContainsKey(serviceType))
        {
            IService service = registeredServices[serviceType];
            if (service is T)
            {
                return service as T;
            }
            else
            {
                Debug.LogWarning($"Service {serviceType.Name} is not of type {typeof(T).Name}");
                return null;
            }
        }
        else
        {
            Debug.LogWarning($"Service {serviceType.Name} is not registered.");
            return null;
        }
    }
}
