using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCube : MonoBehaviour, IClickable
{
    private InputManager inputManager;
    private CountdownManager countdownManager;
    private VisualsManager visualsManager;
    public EventHandler<TaskEventArgs> OnInitializaTask; 
    [SerializeField] private CountdownPurpose purpose;
    private float purposeTimer = 9f;
    
    public void Start()
    {
        inputManager = InputManager.Instance;
        countdownManager = CountdownManager.Instance;
        visualsManager = VisualsManager.Instance;
        inputManager.OnMouseSelect += InputManager_OnSelect;
        RemoveVisual();
    }
    public Transform ObjectTransform
    {
        get
        {
            return transform;
        }
    }
    public GameObject ObjectGameObject
    {
        get
        {
            return gameObject;
        }
    }
    private void InputManager_OnSelect(object sender, EventArgs e)
    {
        if(WasOtherSelected())
        {
            RemoveVisual();
        }
        if(WasSelected())
        {
            
            countdownManager.SpawnPrefab(purposeTimer, purpose);
            SetVisual();
        }
    }
    public bool WasSelected()
    {
        Ray ray = Camera.main.ScreenPointToRay(InputManager.Instance.GetMouseScreenPosition());
        bool WasHit = Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue) && raycastHit.collider.GetComponent<TestCube>() != null;
        return WasHit;
    }
    public bool WasOtherSelected()
    {
        Ray ray = Camera.main.ScreenPointToRay(InputManager.Instance.GetMouseScreenPosition());
        bool WasOtherHit = Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue) && raycastHit.collider.GetComponent<TestCube>() == null;
        return WasOtherHit;
    }
    private void RemoveVisual()
    {
        IClickable interfaceReference = (IClickable) this;
        visualsManager.RemoveVisual(interfaceReference);
    }
    private void SetVisual()
    {
        IClickable interfaceReference = (IClickable) this;
        visualsManager.SetVisual(interfaceReference);

    }
}
