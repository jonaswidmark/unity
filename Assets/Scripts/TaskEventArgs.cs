using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskEventArgs : EventArgs
{
    public CountdownPurpose countdownPurpose {get;}
    public float timer {get;}

    public TaskEventArgs(float timer, CountdownPurpose countdownPurpose)
    {
        this.countdownPurpose = countdownPurpose;
        this.timer = timer;
    }
}
