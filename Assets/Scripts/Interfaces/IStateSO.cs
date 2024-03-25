public interface IStateSO
{
    void EnterState(StateManager stateManager);
    void UpdateState(StateManager stateManager);
    void ExitState(StateManager stateManager);
}