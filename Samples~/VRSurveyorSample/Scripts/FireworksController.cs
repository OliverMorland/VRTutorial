using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireworksController : MonoBehaviour
{
    public ParticleSystem fireworksEffect;
    public AudioSource audioSource;
    public AudioClip fireworksSound;
    public int numberOfFireworks = 5;
    public float intervalBetweenFireworks = 0.5f;

    public void TriggerWaveOfFireworks()
    {
        StartCoroutine(TriggerWaveOfFireworksCoroutine(numberOfFireworks));
    }

    IEnumerator TriggerWaveOfFireworksCoroutine(int numberOfFireworks)
    {
        int counter = 0;
        while (counter <= numberOfFireworks) 
        {
            TriggerFirework();
            yield return new WaitForSeconds(intervalBetweenFireworks);
            counter++;
        }
        
    }

    public void TriggerFirework()
    {
        fireworksEffect.Play();
        audioSource.PlayOneShot(fireworksSound);
    }
}
