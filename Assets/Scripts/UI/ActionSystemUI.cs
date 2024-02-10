using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using UnityEngine;

public class ActionSystemUI : MonoBehaviour
{
    private ActionManager actionManager;
    [SerializeField] private Transform selecionButtonPrefab;
    [SerializeField] private Transform selectionButtonContainerTransform;
    [SerializeField] private TextMeshProUGUI selectionButtonText;
    private List<ActionButtonUI> actionButtonUIList;
    private IClickable selectedTransform;
    private void Awake()
    {
        actionButtonUIList = new List<ActionButtonUI>();
    }
    private void Start()
    {
        ///IService actionIService = ServiceLocator.GetService("ActionService");
        //ActionService actionService = (ActionService)actionIService;
        //ActionService.OnAnySelected += ActionManager_OnAnySelected;
        actionManager = ActionManager.Instance;
        ActionManager.OnAnySelected += ActionManager_OnAnySelected;
        CreateActionButtons();
    }
    private void ActionManager_OnAnySelected(object sender, EventArgs e)
    {
        CreateActionButtons();
    }
    private void CreateActionButtons()
    {
        foreach(Transform buttonTransform in selectionButtonContainerTransform)
        {
            Destroy(buttonTransform.gameObject);
        }
        actionButtonUIList.Clear();
        selectedTransform = actionManager.GetSelectedTransform();
        
        if(selectedTransform != null)
        {
            foreach (BaseAction baseAction in selectedTransform.GetBaseActionArray())
            {
                Transform ActionButtonTransform = Instantiate(selecionButtonPrefab, selectionButtonContainerTransform);
                ActionButtonUI actionButtonUI = ActionButtonTransform.GetComponent<ActionButtonUI>();
                actionButtonUI.SetBaseAction(baseAction);
                actionButtonUIList.Add(actionButtonUI);
            }
        }
    }
}
