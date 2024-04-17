using UnityEngine.Rendering.HighDefinition;

public static class ServiceLocator
{
    /** Used for access to services derived from ServiceManager : Monobehaviour 
        The class Scriptable Objects/ServicesSO/ServiceLocatorSO let's you access Scriptable object Managers
    **/
    //public static InputManager InputManager => InputManager.Instance;
    public static LevelGrid LevelGrid => LevelGrid.Instance;
}