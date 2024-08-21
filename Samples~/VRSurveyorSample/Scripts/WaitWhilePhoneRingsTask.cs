using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTutorial;

namespace VRTutorial.Samples {
	public class WaitWhilePhoneRingsTask : TutorialTask
	{
	    public PhoneRinger phoneRinger;
	    public float ringingDuration = 3f;
	
	    public override void CloseTask()
	    {
	        phoneRinger.StopRinging();
	        StopAllCoroutines();
	    }
	
	    public override void StartTask()
	    {
	        base.StartTask();
	        tutorialStateMachine.ClearNarration();
	        phoneRinger.StartRinging();
	        StartCoroutine(WaitBeforeStoppingRinging());
	    }
	
	    IEnumerator WaitBeforeStoppingRinging()
	    {
	        yield return new WaitForSeconds(ringingDuration);
	        phoneRinger.StopRinging();
	        TryInvokeTaskCompleted();
	    }
	}
}
