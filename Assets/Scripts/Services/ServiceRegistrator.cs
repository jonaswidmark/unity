using UnityEngine;
using System.Collections.Generic;

public class ServiceRegistrator : MonoBehaviour
{
    [SerializeField] private List<BaseService> servicesToRegister;

    private void Awake()
    {
        ServiceManager serviceManager = ServiceManager.Instance;
        if (serviceManager != null)
        {
            foreach (BaseService service in servicesToRegister)
            {
                serviceManager.RegisterService(service.GetServiceName(), service);
            }
        }
    }
}