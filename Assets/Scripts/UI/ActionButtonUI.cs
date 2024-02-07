using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ActionButtonUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMeshPro;
    [SerializeField] private Button button;
    [SerializeField] private IClickable selectedObject;
    private ActionManager actionManager;
    private BaseAction baseAction;

    private void Start()
    {
        actionManager = ActionManager.Instance;
    }
    public void SetBaseAction(BaseAction baseAction)
    {
        this.baseAction = baseAction;
        textMeshPro.text = baseAction.GetActionName().ToUpper();
        
        button.onClick.AddListener(() => {
                actionManager.SetSelectedAction(baseAction);
        });
    } 
    public void UpdateSelectedVisual()
    {
        /*  BaseAction selectedBaseAction = actionManager.GetSelectedAction();
        selectedBaseAction.SetActive(selectedBaseAction == baseAction);  */
    }
}
