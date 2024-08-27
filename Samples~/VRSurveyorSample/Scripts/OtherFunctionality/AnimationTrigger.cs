using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimationTrigger : MonoBehaviour
{
    Animator animator;
    public bool triggerOnEnable = true;
    public string animationToTrigger = "ScaleWithSpringEffect";
    bool isFirstEnable = true;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        if (isFirstEnable)
        {
            isFirstEnable = false;
        }
        else
        {
            if (triggerOnEnable)
            {
                TriggerAnimation();
            }
        }
    }


    [ContextMenu("Trigger animation")]
    public void TriggerAnimation()
    {
        if (animator != null)
        {
            animator.Play(animationToTrigger);
        }
    }
}
