using UnityEngine;

public class ServiceLocatorSO : MonoBehaviour
{
    public static MissionManagerSO MissionManagerSO { get; private set; }
    public static CountdownManagerSO CountdownManagerSO { get; private set; }
    public static EventManagerSO EventManagerSO { get; private set; }
    public static VisualsManagerSO VisualsManagerSO { get; private set; }
    public static GameManagerSO GameManagerSO { get; private set; }
    public static StateManagerSO StateManagerSO { get; private set; }
    public static MouseWorldSO MouseWorldSO { get; private set; }
    //public static CameraManagerSO CameraManagerSO { get; private set; }
    private void OnEnable()
    {
        InitializeManagers();
    }
    public static void InitializeManagers()
    {
        EventManagerSO = Resources.Load<EventManagerSO>("EventManagerSO");
        MissionManagerSO = Resources.Load<MissionManagerSO>("MissionManagerSO");
        CountdownManagerSO = Resources.Load<CountdownManagerSO>("CountdownManagerSO");
        VisualsManagerSO = Resources.Load<VisualsManagerSO>("VisualsManagerSO");
        GameManagerSO = Resources.Load<GameManagerSO>("GameManagerSO");
        //CameraManagerSO = Resources.Load<CameraManagerSO>("CameraManagerSO");
        StateManagerSO = Resources.Load<StateManagerSO>("StateManagerSO");
        MouseWorldSO = Resources.Load<MouseWorldSO>("MouseWorldSO");
    }
}
