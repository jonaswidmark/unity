using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertArrow : MonoBehaviour
{
    public float startAlpha = 0f;
    public float targetAlpha = 1f;
    public float lerpDuration = 1f;
    private SpriteRenderer spriteRenderer;
    private float currentAlpha;
    private float lerpStartTime;
    private float lerpPercentage;
    private bool isLerping = false;
    [SerializeField] Transform targetTransform;
    private bool isActive;
    private void Awake() 
    {
        transform.localPosition = new Vector3(0, 5f, 0);    
    }

    private void Start()
    {
        spriteRenderer = targetTransform.GetComponent<SpriteRenderer>();
        Hide();
    }
    private void Update()
    {
        if (isLerping)  
        {
            // Beräkna andel av LERP-processen som är klar
            float timeSinceLerpStarted = Time.time - lerpStartTime;
            lerpPercentage = Mathf.Clamp01(timeSinceLerpStarted / lerpDuration);

            // Uppdatera alfavärdet baserat på LERP-processen
            currentAlpha = Mathf.Lerp(startAlpha, targetAlpha, lerpPercentage);

            // Uppdatera SpriteRenderer's färg med det nya alfavärdet
            Color color = spriteRenderer.color;
            color.a = currentAlpha;
            spriteRenderer.color = color;

            // Kontrollera om LERP-processen är klar
            if (lerpPercentage >= 1f)
            {
                isLerping = false;
                startAlpha = currentAlpha;
            }
        }
    }
    private void StartLerp()
    {
        lerpStartTime = Time.time;
        isLerping = true;
        lerpPercentage = 0f;
    }
    public void ToggleArrow()
    {
        targetAlpha = (currentAlpha == 0) ? 1 : 0;
        transform.gameObject.SetActive(!isActive);
        StartLerp();
    }
    public void Show()
    {
        isActive = true;
        transform.gameObject.SetActive(isActive);
    }
    public void Hide()
    {
        isActive = false;
        transform.gameObject.SetActive(isActive);
    }
}
