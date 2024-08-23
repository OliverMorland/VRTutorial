using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace VRTutorial.Samples {
	public class ClickUITask : LobbyTutorialTask
	{
	    public Button targetButton;
	    public AudioClip spawnClip;
	    public float spawnTime = 3f;
	
	
	    public override void StartTask()
	    {
	        base.StartTask();
	        lobbyTutorialManager.SetRayInteractorsEnabled(true);
	        StartCoroutine(SpawnTargetButton());
	        targetButton.onClick.AddListener(OnClick);
	    }
	
	    IEnumerator SpawnTargetButton()
	    {
	        yield return new WaitForSeconds(spawnTime);
	        targetButton.gameObject.SetActive(true);
	        lobbyTutorialManager.audioSource.PlayOneShot(spawnClip);
	    }
	
	    void OnClick()
	    {
	        tutorialStateMachine.TriggerValidationFeedback();
	        TryInvokeTaskCompletedWithDelay(standardTaskCompletionDelay);
	    }
	
	    public override void CloseTask()
	    {
	        targetButton.onClick.RemoveListener(OnClick);
	        targetButton.gameObject.SetActive(false);
	    }
	}
	
}
