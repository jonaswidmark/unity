using UnityEngine;
using System;

[CreateAssetMenu(fileName = "New Vector2Args Event", menuName = "Events/Vector2Args")]
public class EventVector2ArgsSO : ScriptableObject
{
    public event EventHandler<Vector2EventArgs> OnRaiseVector2Event;

    public void RaiseEvent(Vector2 vector2Arg)
    {
        OnRaiseVector2Event?.Invoke(this, new Vector2EventArgs(vector2Arg));
    }
}