
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;
using TMPro;
public class Timer : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI timeText;
   
    public void SetTimeText(string timeTextUpdate)
    {
        timeText.text = timeTextUpdate;
    }
    
    
}