using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace VRTutorial.Samples {
	public class ControllerButtonTask : LobbyTutorialTask
	{
	    public InputActionReference leftControllerActionToPerform;
	    public InputActionReference rightControllerActionToPerform;
	    public bool showControllers = true;
	
	    public override void StartTask()
	    {
	        base.StartTask();
	        SetControllerVisualEffectsActive(false);
	        lobbyTutorialManager.SetControllersVisible(showControllers);
	        lobbyTutorialManager.narrator.onNarrationFinished.AddListener(OnNarrationFinished);
	    }
	
	    void OnNarrationFinished()
	    {
	        SetActionToPerform();
	        SetControllerVisualEffectsActive(true);
	        AddControllerListeners();
	        lobbyTutorialManager.narrator.onNarrationFinished.RemoveListener(OnNarrationFinished);
	    }
	
	    private void SetControllerVisualEffectsActive(bool isActive)
	    {
	        TutorialXRControllersManager.Instance.SetVisualEffectActive(isActive);
	    }
	
	    private void AddControllerListeners()
	    {
	        TutorialXRControllersManager.Instance.OnActionPerformed.AddListener(OnCorrectControllerButtonClicked);
	    }
	
	    private void SetActionToPerform()
	    {
	        TutorialXRControllersManager.Instance.SetActionToPerform(leftControllerActionToPerform, rightControllerActionToPerform);
	    }
	
	    private void RemoveControllerListeners()
	    {
	        TutorialXRControllersManager.Instance.OnActionPerformed.RemoveListener(OnCorrectControllerButtonClicked);
	    }
	
	    void OnCorrectControllerButtonClicked()
	    {
	        lobbyTutorialManager.UnhighlightAllButtons();
	        tutorialStateMachine.TriggerValidationFeedback();
	        TryInvokeTaskCompletedWithDelay(standardTaskCompletionDelay);
	    }
	
	    public override void CloseTask()
	    {
	        RemoveControllerListeners();
	        TutorialXRControllersManager.Instance.SetNoActionToPerform();
	    }
	}
	
}
