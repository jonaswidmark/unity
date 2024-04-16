using UnityEngine.Rendering.HighDefinition;

public static class ServiceLocator
{
    /** Used for access to services derived from ServiceManager : Monobehaviour 
        The class Scriptable Objects/ServicesSO/ServiceLocatorSO let's you access Scriptable object Managers
        Todo: Think about if we should have Monobehaviour Managers. Som Managers, e.g. Camera Manager has several references to hierarchy objects
    **/
    public static InputManager InputManager => InputManager.Instance;
    public static LevelGrid LevelGrid => LevelGrid.Instance;
    public static CameraManager CameraManager => CameraManager.Instance;
    //public static MouseWorld MouseWorld => MouseWorld.Instance;
}