public static class ServiceLocator
{
    public static InputManager InputManager => InputManager.Instance;
    public static MissionManager MissionManager => MissionManager.Instance;
    public static StateManager StateManager => StateManager.Instance;
    public static ActionManager ActionManager => ActionManager.Instance;
    public static CountdownManager CountdownManager => CountdownManager.Instance;
    public static GameManager GameManager => GameManager.Instance;
    public static VisualsManager VisualsManager => VisualsManager.Instance;
    public static LevelGrid LevelGrid => LevelGrid.Instance;
    public static EventManager EventManager => EventManager.Instance;
    public static CameraManager CameraManager => CameraManager.Instance;
}