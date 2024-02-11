using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeBase : MonoBehaviour, IClickable
{
    private InputManager inputService;
    private CountdownManager countdownManager;
    private VisualsManager visualsManager;
    private ActionManager actionManager;
    public EventHandler<TaskEventArgs> OnInitializaTask; 
    [SerializeField] private CountdownPurpose purpose;
    
    private BaseAction[] baseActionArray;
    
    private void Awake()
    {
        baseActionArray = GetComponents<BaseAction>();
    }
    private void Start()
    {
        //InputService inputService = ServiceLocator.GetService<InputService>("InputService");
        inputService = InputManager.Instance;
        /* CountdownService countdownService = ServiceLocator.GetService<CountdownService>("CountdownService");
        VisualService visualService = ServiceLocator.GetService<VisualService>("VisualService");
        ActionService actionService = ServiceLocator.GetService<ActionService>("ActionService"); */ 
        countdownManager = CountdownManager.Instance;
        visualsManager = VisualsManager.Instance;
        actionManager = ActionManager.Instance;
        inputService.OnMouseSelect += InputManager_OnSelect;
        
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
    public BaseAction[] GetBaseActionArray()
    {
        return baseActionArray;
    }
    private void InputManager_OnSelect(object sender, EventArgs e)
    {
        if(WasOtherSelected())
        {
            RemoveVisual();
        }
        if(WasSelected())
        {
            SetVisual();
        }
    }
    public bool WasSelected()
    {
        Ray ray = Camera.main.ScreenPointToRay(InputManager.Instance.GetMouseScreenPosition());
        bool WasHit = Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue) && raycastHit.collider.GetComponent<HomeBase>() != null;
        
        if(WasHit)
        {
            actionManager.SetSelectedTransform(this);
        }
        return WasHit;
    }
    public bool WasOtherSelected()
    {
        Ray ray = Camera.main.ScreenPointToRay(InputManager.Instance.GetMouseScreenPosition());
        bool WasOtherHit = Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue) && raycastHit.collider.GetComponent<HomeBase>() == null;
        if(!WasOtherHit)
        {
            actionManager.SetSelectedTransform(null);
        }
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
