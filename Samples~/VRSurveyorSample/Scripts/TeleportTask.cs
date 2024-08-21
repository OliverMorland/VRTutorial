using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;


namespace VRTutorial.Samples {
	public class TeleportTask : LobbyTutorialTask
	{
	    public List<ActionBasedControllerManager> actionBasedControllerManagers;
	    public TeleportationAnchor teleportAnchor;
	    public ColorLerper teleportAnchorColorLerper;
	    public float delayBeforeDisplayingPrompts = 2f;
	
	    public override void StartTask()
	    {
	        base.StartTask();
	        lobbyTutorialManager.ShowTeleportAnchors(true);
	        teleportAnchorColorLerper.SetContinousColorChanging(true);
	        SetTeleportInteractorsEnabled(true);
	        teleportAnchor.teleporting.AddListener(OnTeleporting);
	        lobbyTutorialManager.ShowDesk(false);
	        StartCoroutine(DisplayThumbstickPromptsAfterDelay());
	    }
	
	    IEnumerator DisplayThumbstickPromptsAfterDelay()
	    {
	        yield return new WaitForSeconds(delayBeforeDisplayingPrompts);
	        TutorialXRControllersManager.Instance.SetControllersVisible(true);
	        TutorialXRControllersManager.Instance.HighlightAndAnimateThumbsticks();
	    }
	
	    void SetTeleportInteractorsEnabled(bool isEnabled)
	    {
	        foreach (ActionBasedControllerManager manager in actionBasedControllerManagers)
	        {
	            manager.enabled = isEnabled;
	        }
	    }
	
	    void OnTeleporting(TeleportingEventArgs args)
	    {
	        StopCoroutine(DisplayThumbstickPromptsAfterDelay());
	        tutorialStateMachine.TriggerValidationFeedback();
	        teleportAnchorColorLerper.SetContinousColorChanging(false);
	        teleportAnchorColorLerper.ChangeToTargetColor();
	        TryInvokeTaskCompletedWithDelay(standardTaskCompletionDelay);
	    }
	
	    public override void CloseTask()
	    {
	        StopCoroutine(DisplayThumbstickPromptsAfterDelay());
	        teleportAnchor.teleporting.RemoveListener(OnTeleporting);
	        SetTeleportInteractorsEnabled(false);
	    }
	}
	
}
