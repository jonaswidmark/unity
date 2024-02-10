using UnityEngine;
using System.Collections.Generic;

public class ServiceRegistrator : MonoBehaviour
{
    public List<BaseService> servicesToRegister = new List<BaseService>();

    

    private void Start()
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