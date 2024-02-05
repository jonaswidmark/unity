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
    private BaseAction baseAction;

    public void SetBaseAction(BaseAction baseAction)
    {
        this.baseAction = baseAction;
        textMeshPro.text = baseAction.GetActionName().ToUpper();
        
        /* button.onClick.AddListener(() => {
                ActionSystem.INstance.SetSelectedAction(baseAction);
        }); */
    } 
    public void UpdateSelectedVisual()
    {
        /* BaseAction selectedBaseAction = ActionSystem.Instance.GetSelectedAction();
        selectedBaseAction.SetActive(selectedBaseAction == baseAction); */
    }
}
