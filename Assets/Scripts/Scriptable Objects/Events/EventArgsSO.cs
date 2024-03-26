using UnityEngine;
using System;

[CreateAssetMenu(fileName = "New EventArgs Event", menuName = "Events/EventArgs")]
public class EventArgsSO : ScriptableObject
{
    public event EventHandler<EventArgs> OnRaiseEvent;

    public void RaiseEvent(EventArgs e)
    {
        OnRaiseEvent?.Invoke(this, e);
    }
}