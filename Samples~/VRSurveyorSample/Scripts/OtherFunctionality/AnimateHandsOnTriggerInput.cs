using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction;

public class AnimateHandsOnTriggerInput : MonoBehaviour
{
    public InputActionProperty pinchAnimationAction;
    public InputActionProperty fistAnimationAction;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        //Informing user that they need an animator component
        //Another Comment to test a merge.
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Hand animations will not work, add an animator");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (animator != null)
        {
            float indexTriggerValue = pinchAnimationAction.action.ReadValue<float>();
            animator.SetFloat("Trigger", indexTriggerValue);
            float handTriggerValue = fistAnimationAction.action.ReadValue<float>();
            animator.SetFloat("Grip", handTriggerValue);
        }
    }
}
