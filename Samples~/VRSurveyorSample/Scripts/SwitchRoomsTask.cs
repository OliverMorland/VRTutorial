using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VRTutorial;

namespace VRTutorial.Samples {
	public class SwitchRoomsTask : TutorialTask
	{
	    public Button roomPanelButton;
	    public FeedbackMarker feedbackMarker;
	    public AudioClip markerSound;
	    const string feedbackMarkerText = "Select Button";
	
	    public override void StartTask()
	    {
	        base.StartTask();
	        roomPanelButton.onClick.AddListener(OnRoomPanelButtonClicked);
	        tutorialStateMachine.narrator.onNarrationFinished.AddListener(OnNarrationFinished);
	    }
	
	    void OnNarrationFinished()
	    {
	        feedbackMarker.gameObject.SetActive(true);
	        feedbackMarker.audioSource.PlayOneShot(markerSound);
	        feedbackMarker.SetTextLabel(feedbackMarkerText);
	    }
	
	    void OnRoomPanelButtonClicked()
	    {
	        roomPanelButton.onClick.RemoveListener(OnRoomPanelButtonClicked);
	        tutorialStateMachine.TriggerValidationFeedback();
	        TryInvokeTaskCompletedWithDelay(standardTaskCompletionDelay);
	    }
	
	    public override void CloseTask()
	    {
	        feedbackMarker.gameObject.SetActive(false);
	        tutorialStateMachine.narrator.onNarrationFinished.RemoveListener(OnNarrationFinished);
	        roomPanelButton.onClick.RemoveListener(OnRoomPanelButtonClicked);
	    }
	}
}
