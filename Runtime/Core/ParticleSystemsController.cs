using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemsController : MonoBehaviour
{
    [SerializeField] ParticleSystem [] particleSystems;
    [SerializeField] bool playOnEnable = false;

    private void Awake()
    {
        particleSystems = GetComponentsInChildren<ParticleSystem>();
    }

    private void OnEnable()
    {
        if (playOnEnable)
        {
            Play();
        }
    }

    public void Play()
    {
        foreach (ParticleSystem particleSystem in particleSystems)
        {
            particleSystem.Play();
        }
    }
}
