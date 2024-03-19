using UnityEngine;

public static class ServiceLocator
{/**
    public static IService GetService(string serviceName);
    public static T GetService<T>(string serviceName) where T : class, IService
    {
        ServiceManager serviceManager = ServiceManager.Instance;

        if (serviceManager != null)
        {
            return serviceManager.GetService(serviceName);
            T service = serviceManager.GetService<T>(serviceName);
            if (service != null)
            {
                return service;
            }
            else
            {
                Debug.LogWarning($"Service '{serviceName}' not found or not of type {typeof(T).Name}");
                return null;
            }
        }
        else
        {
            Debug.LogWarning("ServiceManager is not initialized.");
            return null;
        }
    }
    **/
}
