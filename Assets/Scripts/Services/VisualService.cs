
using System;using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualService : MonoBehaviour, IService
{
   
    [SerializeField] private Transform selectedVisual;
    [SerializeField] private GameObject selectedVisualGameObject;
    private Transform visualParent;
    private string SelectedVisualString = "SelectedVisual";
    private IClickable selectedTransform;

    public string GetServiceName()
    {
        return "VisualService";
    }
    public void SetVisual(IClickable clickableObject)
    {
        GameObject parent;
        if (clickableObject.ObjectGameObject.transform.parent == null)
        {
            parent = clickableObject.ObjectGameObject;
        }
        else
        {
            parent = clickableObject.ObjectGameObject.transform.parent.gameObject;
        }
        
        Transform childTransform = clickableObject.ObjectTransform.Find(SelectedVisualString);
        
        if(childTransform)
        {
            childTransform.gameObject.GetComponent<MeshRenderer>().enabled = true;
            selectedTransform = clickableObject;
        }
    }
    public void RemoveVisual(IClickable clickableObject)
    {
        if (clickableObject == null) return;
        if (clickableObject.ObjectTransform.parent == null)
        {
            visualParent = clickableObject.ObjectTransform;
        }
        else
        {
            visualParent = clickableObject.ObjectTransform.parent;
        }
        Transform childTransform = clickableObject.ObjectTransform.Find(SelectedVisualString);
        
        childTransform.gameObject.GetComponent<MeshRenderer>().enabled = false;
        
        if(selectedTransform == clickableObject)
        {
            selectedTransform = null;
        }
    }
    public IClickable GetSelectedTransform()
    {
        return selectedTransform;
    }
   
}
