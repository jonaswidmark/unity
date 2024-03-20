using UnityEngine;
using TMPro;
public class Timer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timeText;
    public void SetTimeText(string timeTextUpdate)
    {
        timeText.text = timeTextUpdate;
    }
}