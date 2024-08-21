using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTutorial;

namespace VRTutorial.Samples {
	public class SubmitViolationTask : TutorialTask
	{
	    public Violation targetViolation;
	
	    public override void StartTask()
	    {
	        base.StartTask();
	        targetViolation.onKTagAssigned.AddListener(OnKTagAssigned);
	    }
	
	    void OnKTagAssigned()
	    {
	        TryInvokeTaskCompletedWithDelay(standardTaskCompletionDelay);
	    }
	
	    public override void CloseTask()
	    {
	        targetViolation.onKTagAssigned.RemoveListener(OnKTagAssigned);
	    }
	}
}
