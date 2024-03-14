using UnityEngine;
using System.Collections.Generic;
using System;
using System.Runtime.InteropServices.WindowsRuntime;


[CreateAssetMenu(fileName = "New MissionTask", menuName = "CustomObjects/MissionTask")]
public class MissionTask : ScriptableObject
{
    [SerializeField] int id;
    [SerializeField] String key;
    [SerializeField] String title;
    [SerializeField] Transform fromTransform;

    [SerializeField] Transform toTransform;

    public int Id
    {
        get { return id; }
        private set { id = value; }
    }
    public string Key
    {
        get { return key; }
        private set { key = value; }
    }
    public string Title
    {
        get { return title; }
        private set { title = value; }
    }
    public Transform FromTransform
    {
        get { return fromTransform; }
        private set { fromTransform = value; }
    }
    public Transform ToTransform
    {
        get { return toTransform; }
        private set { toTransform = value; }
    }
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
