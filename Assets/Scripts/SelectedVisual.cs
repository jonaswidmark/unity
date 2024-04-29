using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedVisual : MonoBehaviour
{
    [SerializeField] Transform parent;
    private float initialPositionY;
    private void Start()
    {
        initialPositionY = parent.transform.position.y;
        Debug.Log("Initial parent y: " + initialPositionY);
    }
    private void Update()
    {
        if (initialPositionY != parent.transform.position.y)
        {
            float adjustedVisualPositionY = initialPositionY - parent.transform.position.y;
            //Debug.Log(initialPositionY - parent.transform.position.y);
            transform.localPosition = new Vector3(transform.localPosition.x,adjustedVisualPositionY,transform.localPosition.z);
        }
    }
}
