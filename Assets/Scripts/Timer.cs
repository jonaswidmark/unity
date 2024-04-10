using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.Rendering.HighDefinition;
public class Timer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timeText;
    float charactersPerSecond = 90;
    public void SetTimeText(string timeTextUpdate, bool typeText)
    {
        if(typeText)
        {
            StartCoroutine(TypeTextUncapped(timeTextUpdate));
        }
        else
        {
            timeText.text = timeTextUpdate;
        }
    }
    IEnumerator TypeTextUncapped(string line)
    {
        float timer = 0;
        float interval = 1 / charactersPerSecond;
        string textBuffer = null;
        char[] chars = line.ToCharArray();
        int i = 0;
        while (i < chars.Length)
                {
                    if (timer < Time.deltaTime)
                    {
                        textBuffer += chars[i];
                        timeText.text=textBuffer;
                        timer += interval;
                        i++;
                    }
                    else
                    {
                        timer -= Time.deltaTime;
                        yield return null;
                    }
                }
    }
}