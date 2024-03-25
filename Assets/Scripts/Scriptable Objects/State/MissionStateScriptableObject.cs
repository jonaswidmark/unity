using UnityEngine;

[CreateAssetMenu(fileName = "New Mission State", menuName = "State Machine/Mission State")]
public class MissionStateScripptableObject : ScriptableObject, IStateSO
{
    public void EnterState(StateManager stateManager)
    {
        // Lägg till kod för att hantera inträdet i Idle State
    }

    public void UpdateState(StateManager stateManager)
    {
        // Lägg till kod för att hantera uppdateringen i Idle State
    }

    public void ExitState(StateManager stateManager)
    {
        // Lägg till kod för att hantera utträdet från Idle State
    }
}