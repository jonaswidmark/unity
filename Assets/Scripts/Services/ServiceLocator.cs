using UnityEngine;

public static class ServiceLocator
{
    public static IService GetService(string serviceName)
    {
        ServiceManager serviceManager = ServiceManager.Instance;

        if (serviceManager != null)
        {
            return serviceManager.GetService(serviceName);
        }
        else
        {
            Debug.LogWarning("ServiceManager is not initialized.");
            return null;
        }
    }
}
