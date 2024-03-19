using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    private static Manager instance;
    private Dictionary<string, IService> services = new Dictionary<string, IService>();

    public static Manager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameObject("ServiceManager").AddComponent<Manager>();
                DontDestroyOnLoad(instance.gameObject);
            }
            return instance;
        }
    }

    public void RegisterService(string serviceName, IService service)
    {
        if (!services.ContainsKey(serviceName))
        {
            services.Add(serviceName, service);
        }
    }

    public T GetService<T>(string serviceName) where T : IService
    {
        if (services.ContainsKey(serviceName))
        {
            return (T)services[serviceName];
        }
        else
        {
            Debug.LogError("Service not found: " + serviceName);
            return default(T);
        }
    }
}
