using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Narration", menuName = "ScriptableObjects/Narration", order = 1)]
public class NarrationSO : ScriptableObject
{
    public AudioClip audio;
    [TextArea(2, 5)]
    public string text;
}
