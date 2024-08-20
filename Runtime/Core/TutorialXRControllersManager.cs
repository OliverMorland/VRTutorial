using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class TutorialXRControllersManager : MonoBehaviour
{
    public ControllerTasksSetter leftControllerTasksSetter;
    public ControllerTasksSetter rightControllerTasksSetter;
    public UnityEvent OnActionPerformed;
    public bool areControllersVisibleOnAwake = false;
    public static TutorialXRControllersManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
        leftControllerTasksSetter.OnActionToPerformAccomplished.AddListener(TryInvokeOnActionToPerfomAccomplished);
        rightControllerTasksSetter.OnActionToPerformAccomplished.AddListener(TryInvokeOnActionToPerfomAccomplished);
        SetControllersVisible(areControllersVisibleOnAwake);
    }

    public void SetActionToPerform(InputActionReference leftControllerAction, InputActionReference rightControllerAction)
    {
        leftControllerTasksSetter.SetActionToPerform(leftControllerAction);
        rightControllerTasksSetter.SetActionToPerform(rightControllerAction);
    }

    public void SetNoActionToPerform()
    {
        leftControllerTasksSetter.SetNoActionToPerform();
        rightControllerTasksSetter.SetNoActionToPerform();
    }

    public void SetControllersVisible(bool areControllersVisible)
    {
        leftControllerTasksSetter.SetControllerVisibleState(areControllersVisible);
        rightControllerTasksSetter.SetControllerVisibleState(areControllersVisible);
    }

    public void HighlightAndAnimateThumbsticks()
    {
        leftControllerTasksSetter.HighlightAndAnimateThumbstick();
        rightControllerTasksSetter.HighlightAndAnimateThumbstick();
    }

    public void UnhighlightAllButtons()
    {
        leftControllerTasksSetter.UnhighlightAllButtons();
        rightControllerTasksSetter.UnhighlightAllButtons();
    }

    public void SetVisualEffectActive(bool isActive)
    {
        leftControllerTasksSetter.SetVisualEffectActive(isActive);
        rightControllerTasksSetter.SetVisualEffectActive(isActive);
    }

    void TryInvokeOnActionToPerfomAccomplished()
    {
        if (OnActionPerformed != null)
        {
            OnActionPerformed.Invoke();
        }
    }
}
