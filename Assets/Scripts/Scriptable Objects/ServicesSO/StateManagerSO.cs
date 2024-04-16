using System;
using UnityEngine;
[CreateAssetMenu(fileName = "StateManagerSO", menuName = "ServicesSO/StateManagerSO")]
public class StateManagerSO: ScriptableObject
{
    private IStateSO currentState;
    public event EventHandler OnMissionState;
    public event EventHandler OnIdleState;
    [SerializeField] private IdleStateScripptableObject IdleState;
    [SerializeField] private MissionStateScripptableObject MissionState;

    private void Start()
    {
        SetCurrentState(IdleState);
    }
    public void SetIdleState()
    {
        SetCurrentState(IdleState);
    }
    
    private void SetCurrentState(IStateSO newState)
    {
        currentState = newState;
        if(newState.GetType() == typeof(MissionStateScripptableObject))
        {
            OnMissionState?.Invoke(this, EventArgs.Empty);
        }
        if(newState.GetType() == typeof(IdleStateScripptableObject))
        {
            OnIdleState?.Invoke(this, EventArgs.Empty);
        }
        currentState.EnterState(this);
    }
    
    public IStateSO GetPlayerState()
    {
        return currentState;
    }
    public void ChangeState(IStateSO newState)
    {
        
        if (currentState != null)
        {
            currentState.ExitState(this);
        }
        SetCurrentState(newState);
    }
}

