using UnityEngine;
using System.Collections.Generic;

public class ServiceListContainer : MonoBehaviour
{
    [SerializeField] private List<IService> serviceList = new List<IService>();

    public List<IService> ServiceList
    {
        get { return serviceList; }
    }
}
