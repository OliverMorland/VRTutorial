using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VRTutorial;

namespace VRTutorial.Samples {
	public class OpenTabletCameraTask : TutorialTask
	{
	    public TabletManager tablet;
	    public Button cameraButton;
	
	    public override void StartTask()
	    {
	        base.StartTask();
	        tablet.UnlockTablet();
	        cameraButton.onClick.AddListener(OnButtonClicked);
	    }
	
	    void OnButtonClicked()
	    {
	        tutorialStateMachine.TriggerValidationFeedback();
	        TryInvokeTaskCompletedWithDelay(standardTaskCompletionDelay);
	    }
	
	    public override void CloseTask()
	    {
	        cameraButton.onClick.RemoveListener(OnButtonClicked);
	    }
	}
}
