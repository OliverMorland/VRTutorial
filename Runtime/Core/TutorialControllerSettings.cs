using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Tutorial Controller Settings", menuName = "ScriptableObjects/TutorialControllerSettings", order = 1)]

public class TutorialControllerSettings : ScriptableObject
{
    public Color normalColor;
    public Color highlightedColor;
    public float impulseStrength = 0.1f;
    public float impulseDuration = 0.1f;
}
