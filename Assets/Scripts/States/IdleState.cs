using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{
    void Enter();
    void Update();
    void Exit();
}

public class IdleState : IState
{
    private MissionManager missionManager = ServiceLocator.MissionManager;
    public void Enter()
    {
        // Logik för att gå in i tillståndet "Idle"
    }

    public void Update()
    {
        // Logik som körs varje frame medan i tillståndet "Idle"
    }

    public void Exit()
    {
        missionManager.UpdateMissionList();
    }
}

// Definiera fler tillstånd på samma sätt...

