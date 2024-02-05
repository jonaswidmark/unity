using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IClickable
{
    Transform ObjectTransform { get; }
    GameObject ObjectGameObject { get; }
    public bool WasSelected();
}
