using UnityEngine;

[CreateAssetMenu(fileName = "New Mission State", menuName = "State Machine/Mission State")]
public class MissionStateScripptableObject : ScriptableObject, IStateSO
{
    public void EnterState(StateManagerSO stateManager)
    {
        // Lägg till kod för att hantera inträdet i Idle State
    }

    public void UpdateState(StateManagerSO stateManager)
    {
        // Lägg till kod för att hantera uppdateringen i Idle State
    }

    public void ExitState(StateManagerSO stateManager)
    {
        // Lägg till kod för att hantera utträdet från Idle State
    }
}