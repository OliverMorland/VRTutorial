using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.XR.Interaction.Toolkit;

namespace VRTutorial.Samples {
	public class LobbyGrabTask : LobbyTutorialTask
	{
	    public XRGrabInteractable objectToGrab;
	    const float delayToConfirmGrabHasOccurred = 0.3f;
	    const float taskCompletionDelay = 1f;
	
	    public override void StartTask()
	    {
	        base.StartTask();
	        objectToGrab.gameObject.SetActive(true);
	        objectToGrab.selectEntered.AddListener(OnObjectGrabbed);
	        lobbyTutorialManager.SetControllersVisible(false);
	    }
	
	    void OnObjectGrabbed(SelectEnterEventArgs args)
	    {
	        StartCoroutine(WaitToCheckIfObjectIsStillHeld());
	    }
	
	    IEnumerator WaitToCheckIfObjectIsStillHeld()
	    {
	        yield return new WaitForSeconds(delayToConfirmGrabHasOccurred);
	        if (objectToGrab.isSelected)
	        {
	            tutorialStateMachine.TriggerValidationFeedback();
	            lobbyTutorialManager.SpawnVisualEffectAt(objectToGrab.gameObject.transform.position);
	            TryInvokeTaskCompletedWithDelay(taskCompletionDelay);
	        }
	    }
	
	    public override void CloseTask()
	    {
	        objectToGrab.gameObject.SetActive(false);
	        objectToGrab.selectEntered.RemoveListener(OnObjectGrabbed);
	    }
	
	
	}
}
