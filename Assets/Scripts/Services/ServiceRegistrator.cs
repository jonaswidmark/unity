using System.Collections.Generic;
using UnityEngine;

public class ServiceRegistrator : MonoBehaviour
{
    [SerializeField] private List<ConcreteService> servicesToRegister = new List<ConcreteService>();

    private void Start()
    {
        ServiceManager serviceManager = ServiceManager.Instance;
        if (serviceManager != null)
        {
            foreach (ConcreteService service in servicesToRegister)
            {
                serviceManager.RegisterService(service.GetServiceName(), service);
            }
        }
    }
}