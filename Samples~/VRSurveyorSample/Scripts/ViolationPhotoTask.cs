using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTutorial;

namespace VRTutorial.Samples {
	public class ViolationPhotoTask : TutorialTask
	{
	    public TabletManager tablet;
	    public Violation targetViolation;
	
	    public override void StartTask()
	    {
	        base.StartTask();
	        tablet.OnViolationPhotographed.AddListener(OnViolationPhotographed);
	    }
	
	    void OnViolationPhotographed(Violation violation)
	    {
	        if (violation == targetViolation)
	        {
	
	            TryInvokeTaskCompletedWithDelay(standardTaskCompletionDelay);
	        }
	    }
	
	    public override void CloseTask()
	    {
	        tablet.OnViolationPhotographed.RemoveListener(OnViolationPhotographed);
	    }
	}
}
