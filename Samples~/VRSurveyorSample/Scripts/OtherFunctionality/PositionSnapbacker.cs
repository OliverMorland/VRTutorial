using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent (typeof(XRGrabInteractable))]
public class PositionSnapbacker : MonoBehaviour
{
    public Collider floorCollider;
    public float snapbackDelay = 1.5f;
    public AudioClip spawnSound;
    public ParticleSystem particleSystem;
    Vector3 startPosition;
    Rigidbody rigidBody;
    AudioSource audioSource;
    XRGrabInteractable grabInteractable; 

    private void Awake()
    {
        startPosition = transform.position;
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        grabInteractable = GetComponent<XRGrabInteractable>();
    }

    public void ReturnToStartPosition()
    {
        transform.position = startPosition;
        rigidBody.velocity = Vector3.zero;
        rigidBody.angularVelocity = Vector3.zero;
        if (spawnSound != null)
        {
            audioSource.PlayOneShot(spawnSound);
        }
        if (particleSystem != null)
        {
            particleSystem.Play();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!grabInteractable.isSelected)
        {
            if (floorCollider != null && floorCollider == collision.collider)
            {
                StartCoroutine(ReturnToStartPositionAfterDelay());
            }
        }
    }

    IEnumerator ReturnToStartPositionAfterDelay()
    {
        yield return new WaitForSeconds(snapbackDelay);
        if (!grabInteractable.isSelected)
        {
            ReturnToStartPosition();
        }
    }
}
