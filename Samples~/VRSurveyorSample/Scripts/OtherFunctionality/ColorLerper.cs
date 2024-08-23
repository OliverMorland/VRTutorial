using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorLerper : MonoBehaviour
{
    public Renderer[] materialRenderers;
    public float duration;
    public Color targetColor;
    public string colorPropertyName;
    public bool continouslyChangeColor = false;
    Color startColor;

    private void Start()
    {
        foreach (var materialRenderer in materialRenderers)
        {
            if (materialRenderer.material.HasColor(colorPropertyName))
            {
                startColor = materialRenderer.material.GetColor(colorPropertyName);
            }
        }
    }

    [ContextMenu("Change To Target Color")]
    public void ChangeToTargetColor()
    {
        foreach (var materialRenderer in materialRenderers)
        {
            if (materialRenderer.material.HasColor(colorPropertyName))
            {
                StartCoroutine(ChangeToTargetColorOverTime());
            }
        }
    }

    IEnumerator ChangeToTargetColorOverTime()
    {
        float timeElapsed = 0;
        while (timeElapsed < duration)
        {
            Color lerpedColor = Color.Lerp(startColor, targetColor, timeElapsed / duration);
            foreach (var materialRenderer in materialRenderers)
            {
                materialRenderer.material.SetColor(colorPropertyName, lerpedColor);
            }
            timeElapsed += Time.deltaTime;
            yield return null;
        }
    }

    private void Update()
    {
        if (continouslyChangeColor)
        {
            float speed = 1 / duration;
            float sinValue = 0.5f + 0.5f * Mathf.Sin(2f * Mathf.PI * Time.time * speed);
            Color lerpedColor = Color.Lerp(startColor, targetColor, sinValue);
            foreach (var materialRenderer in materialRenderers)
            {
                materialRenderer.material.SetColor(colorPropertyName, lerpedColor);
            }
        }
    }

    public void SetContinousColorChanging(bool isOn)
    {
        continouslyChangeColor = isOn;
    }
}
