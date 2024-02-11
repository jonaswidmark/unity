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
        // Logik för att lämna tillståndet "Idle"
    }
}

// Definiera fler tillstånd på samma sätt...

