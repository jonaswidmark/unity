using System.Collections.Generic;
using UnityEngine;

public class ServiceRegistrator : MonoBehaviour
{
    [SerializeField] private List<ConcreteService> servicesToRegister = new List<ConcreteService>();

    private void Awake()
    {
        Debug.Log("ServiceRegistrator");
    }
    private void Start()
    {
        Debug.Log("Här då?");
        ServiceManager serviceManager = ServiceManager.Instance;
        
        Debug.Log(serviceManager);
        if (serviceManager != null)
        {
            foreach (ConcreteService service in servicesToRegister)
            {
                serviceManager.RegisterService(service.GetServiceName(), service);
            }
        }
    }
}
