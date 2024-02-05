using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using UnityEngine;

public class ActionSystemUI : MonoBehaviour
{
    private VisualsManager visualsManager;
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
        visualsManager = VisualsManager.Instance;
        VisualsManager.OnAnySelected += VisualsManager_OnAnySelected;
        CreateActionButtons();
    }
    private void VisualsManager_OnAnySelected(object sender, EventArgs e)
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
        selectedTransform = visualsManager.GetSelectedTransform();
        
        if(selectedTransform != null)
        {
            foreach (BaseAction baseAction in selectedTransform.GetBaseActionArray())
            {
                Debug.Log(baseAction);
                Transform ActionButtonTransform = Instantiate(selecionButtonPrefab, selectionButtonContainerTransform);
                ActionButtonUI actionButtonUI = ActionButtonTransform.GetComponent<ActionButtonUI>();
                actionButtonUI.SetBaseAction(baseAction);
                actionButtonUIList.Add(actionButtonUI);
            }
        }
        
    }
}
