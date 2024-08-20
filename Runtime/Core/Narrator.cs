using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Narrator : MonoBehaviour
{
    public NarrationSO narration;
    public AudioSource audioSource;
    public TMP_Text caption;
    public bool playOnStart = false;
    float narrationDuration;
    int letterCount;
    public UnityEvent onNarrationFinished;

    private void Start()
    {
        caption.text = "";
        if (playOnStart)
        {
            PlayNarration();
        }
    }

    public void SetNarration(NarrationSO desiredNarration)
    {
        narration = desiredNarration;
        audioSource.clip = narration.audio;
    }

    public void PlayNarration()
    {
        if (narration != null)
        {
            audioSource.Play();
            narrationDuration = narration.audio.length;
            letterCount = narration.text.Length;
            StartCoroutine(RevealText());
        }
        else
        {
            Debug.LogError("No narration");
        }
    }

    IEnumerator RevealText()
    {
        caption.text = "";
        float elapsedTime = 0;
        while (elapsedTime < narrationDuration)
        {
            yield return null;
            elapsedTime += Time.deltaTime;
            float percentageElapsed = elapsedTime / narrationDuration;
            float proportionOfTextToDisplay = Mathf.Lerp(0, (float)letterCount, percentageElapsed);
            int lettersToDisplay = (int)proportionOfTextToDisplay;
            int startCharacterIndex = 0;
            caption.text = narration.text.Substring(startCharacterIndex, lettersToDisplay);
        }
        if (onNarrationFinished != null)
        {
            onNarrationFinished.Invoke();
        }
    }

    bool IsPunctuation(char c)
    {
        if (c == '.' || c == '!' || c == '?' || c == ',')
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void ClearNarration()
    {
        StopAllCoroutines();
        audioSource.Stop();
        caption.text = "";
    }

}
