using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR;
using Unity.VisualScripting;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using UnityEngine.VFX;

public class ControllerTasksSetter : MonoBehaviour
{
    public XRBaseController controller;
    public InputActionReference handTriggerAction;
    public InputActionReference triggerAction;
    public InputActionReference teleportAction;
    public Renderer handTrigger;
    public Renderer trigger;
    public Renderer thumbstick;
    public GameObject handModel;
    public GameObject controllerModel;
    public Animator controllerAnimator;
    public ParticleSystemsController confirmationEffect;
    public TutorialControllerSettings tutorialControllerSettings;
    InputActionReference currentActionToPerform;
    public UnityEvent OnActionToPerformAccomplished;
    
    private void Start()
    {
        UnhighlightAllButtons();
        handTriggerAction.action.performed += OnActionPerformed;
        triggerAction.action.performed += OnActionPerformed;
        teleportAction.action.performed += OnActionPerformed;
    }

    private void OnActionPerformed(InputAction.CallbackContext obj)
    {
        if (currentActionToPerform != null)
        {
            if (obj.action == currentActionToPerform.action)
            {
                confirmationEffect.Play();
                if (OnActionToPerformAccomplished != null)
                {
                    OnActionToPerformAccomplished.Invoke();
                }
            }
        }
    }

    public void UnhighlightAllButtons()
    {
        SetControllerButtonHighlight(handTrigger, false);
        SetControllerButtonHighlight(trigger, false);
        SetControllerButtonHighlight(thumbstick, false);
    }

    public void SetActionToPerform(InputActionReference actionToPerform)
    {
        currentActionToPerform = actionToPerform;
        if (currentActionToPerform == handTriggerAction)
        {
            SetControllerButtonHighlight(handTrigger, true);
        }
        else if (currentActionToPerform == triggerAction)
        {
            SetControllerButtonHighlight(trigger, true);
        }
        else if (currentActionToPerform ==  teleportAction)
        {
            SetControllerButtonHighlight(thumbstick, true);
            SetThumbstickAnimation();
        }
    }

    public void SetTriggerActionToPerform()
    {
        currentActionToPerform = triggerAction;
        SetControllerButtonHighlight(trigger, true);
    }

    public void SetHandTriggerActionToPerform()
    {
        currentActionToPerform = handTriggerAction;
        SetControllerButtonHighlight(handTrigger, true);
    }

    public void SetThumbstickActionToPerform()
    {
        currentActionToPerform = teleportAction;
        SetControllerButtonHighlight(thumbstick, true);
        SetThumbstickAnimation();
    }

    public void SetFistActionToPerform()
    {
        currentActionToPerform = handTriggerAction;
    }

    public void SetNoActionToPerform()
    {
        currentActionToPerform = null;
        SetControllerAnimationsNeutral();
    }

    public void SetControllerVisibleState(bool isControllerVisible)
    {
        controllerModel.SetActive(isControllerVisible);
        handModel.SetActive(!isControllerVisible);
        if (!isControllerVisible)
        {
            confirmationEffect.transform.position = handModel.transform.position;
        }
    }

    private void SetControllerButtonHighlight(Renderer controllerButton, bool isHighlighted)
    {
        controller.SendHapticImpulse(tutorialControllerSettings.impulseStrength, tutorialControllerSettings.impulseDuration);
        if (isHighlighted)
        {
            SetButtonEmissionColor(controllerButton, tutorialControllerSettings.highlightedColor);
        }
        else
        {
            SetButtonEmissionColor(controllerButton, tutorialControllerSettings.normalColor);
        }
    }

    private void SetButtonEmissionColor(Renderer controllerButton, Color desiredColor)
    {
        if (controllerButton.material.HasColor("_EmissionColor"))
        {
            controllerButton.material.SetColor("_EmissionColor", desiredColor);
        }
    }

    public void SetVisualEffectActive(bool isActive)
    {
        confirmationEffect.gameObject.SetActive(isActive);
    }

    public void HighlightAndAnimateThumbstick()
    {
        SetControllerButtonHighlight(thumbstick, true);
        SetThumbstickAnimation();
    }

    void SetThumbstickAnimation()
    {
        controllerAnimator.SetBool("isThumbstick", true);
    }

    private void SetControllerAnimationsNeutral()
    {
        controllerAnimator.SetBool("isThumbstick", false);
    }
}
