using UnityEngine;

public abstract class ServiceManager<T> : MonoBehaviour where T : ServiceManager<T>
{
    private static T instance;

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<T>();
                if (instance == null)
                {
                    Debug.LogError($"No instance of {typeof(T)} found in the scene.");
                }
            }
            return instance;
        }
    }
}
