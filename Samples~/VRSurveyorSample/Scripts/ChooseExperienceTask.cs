using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace VRTutorial.Samples {
	public class ChooseExperienceTask : LobbyTutorialTask
	{
	    public float recommendedExperienceAnimationDelay = 4f;
	    public float scalingAmplitude = 1.1f;
	    public float animationSpeed = 1f;
	    public GameObject targetPanel;
	    Vector3 startScale;
	
	    public override void StartTask()
	    {
	        base.StartTask();
	        lobbyTutorialManager.SetRayInteractorsEnabled(true);
	        lobbyTutorialManager.SetControllersVisible(false);
	        lobbyTutorialManager.experiencePanelsParent.gameObject.SetActive(true);
	        startScale = targetPanel.transform.localScale;
	        LobbyTutorialManager.Instance.narrator.onNarrationFinished.AddListener(OnNarratorFinished);
	    }
	
	    void OnNarratorFinished()
	    {
	        LobbyTutorialManager.Instance.narrator.onNarrationFinished.RemoveListener(OnNarratorFinished);
	        AnimateTargetPanel();
	        TryInvokeTaskCompleted();
	    }
	
	    void AnimateTargetPanel()
	    {
	        StartCoroutine(OscillateExperiencePanelSize());
	    }
	
	    IEnumerator OscillateExperiencePanelSize() 
	    {
	        float elapsedTime = 0;
	        const float animationDuration = 1f;
	        while (elapsedTime < animationDuration)
	        {
	            Vector3 sizeChange = startScale * scalingAmplitude * Mathf.Sin(2f * Mathf.PI * elapsedTime * animationSpeed);
	            targetPanel.transform.localScale = startScale + sizeChange;
	            elapsedTime += Time.deltaTime;
	            yield return null;
	        }
	    }
	
	    public override void CloseTask()
	    {
	        LobbyTutorialManager.Instance.narrator.onNarrationFinished.RemoveListener(OnNarratorFinished);
	    }
	}
}
