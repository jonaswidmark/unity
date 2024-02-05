using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualsManager : MonoBehaviour
{
    public static VisualsManager Instance {get; private set;}
    [SerializeField] private Transform selectedVisual;
    [SerializeField] private GameObject selectedVisualGameObject;
    private Transform visualParent;
    private string SelectedVisualString = "SelectedVisual";

    public void Awake()
    {
        if(Instance != null)
        {
            Debug.Log("There's more than one VisualsManager! " + transform + " - " + Instance);
            Destroy(gameObject);
            return ;
        }
        Instance = this;
       
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
        }
        
    }
    public void RemoveVisual(IClickable clickableObject)
    {
        
        
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
    }
   
}
