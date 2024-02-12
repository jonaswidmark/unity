using UnityEngine;
using System.Collections.Generic;
using System;
using System.Runtime.InteropServices.WindowsRuntime;


[CreateAssetMenu(fileName = "New CountdownPurpose", menuName = "CustomObjects/CountdownPurpose")]
public class CountdownPurpose : ScriptableObject, ITask
{
    [SerializeField] int id;
    [SerializeField] String key;
    [SerializeField] String title;

    public int GetId()
    {
        return id;
    }
    public string GetKey()
    {
        return key;
    }
    public string GetTitle()
    {
        return title;
    }
    
}
