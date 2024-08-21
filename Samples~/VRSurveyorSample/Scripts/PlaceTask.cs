using System.Collections.Generic;
using UnityEngine;

namespace VRTutorial.Samples {
	public class PlaceTask : LobbyTutorialTask
	{
	    public GameObject ballPlacingObjects;
	    public BallDetector ballDetector;
	    public List<PositionSnapbacker> tutorialBalls;
	    const float taskCompletionDelay = 1f;
	    public int targetBallDrops = 3;
	    public int currentBallDrops;
	
	    public override void StartTask()
	    {
	        base.StartTask();
	        ballPlacingObjects.SetActive(true);
	        ballDetector.onBallDroppedInZone.AddListener(OnBallDroppedInZone);
	        ballDetector.onTargetBallCountReached.AddListener(OnTargetBallCountReached);
	        lobbyTutorialManager.SetControllersVisible(false);
	        ResetBalls();
	    }
	
	    private void ResetBalls()
	    {
	        ballDetector.enteredBalls = new List<GameObject>();
	        foreach (PositionSnapbacker ball in tutorialBalls)
	        {
	            ball.ReturnToStartPosition();
	        }
	    }
	
	    void OnBallDroppedInZone()
	    {
	        tutorialStateMachine.TriggerValidationFeedback();
	        Transform lastBall = ballDetector.enteredBalls[ballDetector.enteredBalls.Count - 1].transform;
	        if (lastBall != null)
	        {
	            lobbyTutorialManager.SpawnVisualEffectAt(lastBall.position);
	        }
	    }
	
	    void OnTargetBallCountReached()
	    {
	        TryInvokeTaskCompletedWithDelay(taskCompletionDelay);
	    }
	
	    public override void CloseTask()
	    {
	        currentBallDrops = 0;
	        ballPlacingObjects.SetActive(false);
	        ballDetector.onBallDroppedInZone.RemoveListener(OnBallDroppedInZone);
	        ballDetector.onTargetBallCountReached.RemoveListener(OnTargetBallCountReached);
	    }
	
	}
}
