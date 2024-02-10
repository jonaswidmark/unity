
public interface IService
{
    string GetServiceName();
}

[System.Serializable]
public class ConcreteService : IService
{
    public string serviceName;

    public string GetServiceName()
    {
        return serviceName;
    }
}