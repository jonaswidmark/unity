using UnityEngine;
using System;

[CreateAssetMenu(fileName = "New Game Event", menuName = "Events/Game Event")]
public class GameEvent<T> : ScriptableObject
{
    public event Action<T> OnEventRaised;

    public void RaiseEvent(T data)
    {
        OnEventRaised?.Invoke(data);
    }
}
