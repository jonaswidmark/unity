public interface IStateSO
{
    void EnterState(StateManagerSO stateManager);
    void UpdateState(StateManagerSO stateManager);
    void ExitState(StateManagerSO stateManager);
}