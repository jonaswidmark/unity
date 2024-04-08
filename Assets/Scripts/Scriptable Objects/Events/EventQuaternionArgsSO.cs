using UnityEngine;
using System;

[CreateAssetMenu(fileName = "New QuaternionArgs Event", menuName = "Events/QuaternionArgs")]
public class EventQuaternionArgsSO : ScriptableObject
{
    public event EventHandler<QuaternionEventArgs> OnRaiseQuaternionEvent;
    public void RaiseEvent(Quaternion quaternionArg)
    {
        OnRaiseQuaternionEvent?.Invoke(this, new QuaternionEventArgs(quaternionArg));
    }
}