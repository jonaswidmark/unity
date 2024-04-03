using UnityEngine;
using System;

[CreateAssetMenu(fileName = "New StringArgs Event", menuName = "Events/StringArgs")]
public class EventStringArgsSO : ScriptableObject
{
    public event EventHandler<StringEventArgs> OnRaiseStringEvent;

    public void RaiseEvent(string stringArg)
    {
        OnRaiseStringEvent?.Invoke(this, new StringEventArgs(stringArg));
    }
}