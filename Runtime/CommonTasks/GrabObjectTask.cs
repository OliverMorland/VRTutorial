using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace VRTutorial
{
    public class GrabObjectTask : TutorialTask
    {
        public XRGrabInteractable objectToGrab;
        const float delayToConfirmGrabHasOccurred = 0.3f;
        const float taskCompletionDelay = 1f;

        public override void StartTask()
        {
            base.StartTask();
            objectToGrab.gameObject.SetActive(true);
            objectToGrab.selectEntered.AddListener(OnObjectGrabbed);
        }

        void OnObjectGrabbed(SelectEnterEventArgs args)
        {
            StartCoroutine(WaitToCheckIfObjectIsStillHeld());
        }

        IEnumerator WaitToCheckIfObjectIsStillHeld()
        {
            yield return new WaitForSeconds(delayToConfirmGrabHasOccurred);
            if (objectToGrab.isSelected)
            {
                tutorialStateMachine.TriggerValidationFeedback();
                TryInvokeTaskCompletedWithDelay(taskCompletionDelay);
            }
        }

        public override void CloseTask()
        {
            objectToGrab.selectEntered.RemoveListener(OnObjectGrabbed);
        }
    }
}
