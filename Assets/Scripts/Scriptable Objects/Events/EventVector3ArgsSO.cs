using UnityEngine;
using System;

[CreateAssetMenu(fileName = "New Vector3Args Event", menuName = "Events/Vector3Args")]
public class EventVector3ArgsSO : ScriptableObject
{
    public event EventHandler<Vector3EventArgs> OnRaiseVector3Event;
    public void RaiseEvent(Vector3 vector3Arg)
    {
        OnRaiseVector3Event?.Invoke(this, new Vector3EventArgs(vector3Arg));
    }
}