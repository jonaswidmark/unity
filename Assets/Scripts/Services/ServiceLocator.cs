public static class ServiceLocator
{
    public static InputManager InputManager => InputManager.Instance;
    public static MissionManager MissionManager => MissionManager.Instance;
    public static StateMachine StateMachine => StateMachine.Instance;
    public static ActionManager ActionManager => ActionManager.Instance;
    public static CountdownManager CountdownManager => CountdownManager.Instance;
    public static GameManager GameManager => GameManager.Instance;
    public static VisualsManager VisualsManager => VisualsManager.Instance;
    public static LevelGrid LevelGrid => LevelGrid.Instance;
}