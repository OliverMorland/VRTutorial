using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTutorial;

namespace VRTutorial.Samples {
	public class ResolveRoomHazardTask : TutorialTask
	{
	    public RoomHazard targetRoomHazard;
	    public GameObject vortex;
	
	    public override void StartTask()
	    {
	        base.StartTask();
	        tutorialStateMachine.narrator.onNarrationFinished.AddListener(OnNarrationFinished);
	        targetRoomHazard.OnHazardResolved.AddListener(OnHazardResolved);
	    }
	
	    void OnNarrationFinished()
	    {
	        if (vortex != null)
	        {
	            vortex.SetActive(true);
	        }
	    }
	
	    void OnHazardResolved(bool isResolved)
	    {
	        if (isResolved)
	        {
	            targetRoomHazard.OnHazardResolved.RemoveListener(OnHazardResolved);
	            tutorialStateMachine.TriggerValidationFeedback();
	            tutorialStateMachine.StartNextTask();
	        }
	    }
	
	    public override void CloseTask()
	    {
	        tutorialStateMachine.narrator.onNarrationFinished.RemoveListener(OnNarrationFinished);
	        targetRoomHazard.OnHazardResolved.RemoveListener(OnHazardResolved);
	    }
	}
}
