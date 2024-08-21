using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VRTutorial.Samples {
	public class PhotoTakingTask : LobbyTutorialTask
	{
	    public TabletManager tablet;
	    public GameObject ballsAndBoxes;
	
	    public override void StartTask()
	    {
	        base.StartTask();
	        lobbyTutorialManager.SetControllersVisible(false);
	        ballsAndBoxes.gameObject.SetActive(true);
	        tablet.gameObject.SetActive(true);
	        tablet.TurnToFirstPage();
	        tablet.PlayButtonClickSound();
	        tablet.SendHapticImpulse();
	        tablet.OnAllViolationsSubmitted.AddListener(OnAllViolationsSubmitted);
	    }
	
	    void OnAllViolationsSubmitted()
	    {
	        tutorialStateMachine.TriggerValidationFeedback();
	        TryInvokeTaskCompletedWithDelay(standardTaskCompletionDelay);
	    }
	
	    public override void CloseTask()
	    {
	        tablet.OnAllViolationsSubmitted.RemoveListener(OnAllViolationsSubmitted);
	        ballsAndBoxes.gameObject.SetActive(false);
	        tablet.gameObject.SetActive(false);
	    }
	}
}
