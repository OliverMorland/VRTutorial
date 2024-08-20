using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace VRTutorial
{
    public class TeleportNearLocation : TutorialTask
    {
        public Transform targetPoint;
        public TeleportationArea teleportArea;
        public TeleportationProvider teleportationProvider;
        public GameObject remedyingItems;
        [Range(0, 5f)] public float delayBeforeShowingRemedyingItems = 1f;
        [Range(0, 3f)] public float thresholdDistanceFromTarget = 1.5f;
        public float delayBeforeDisplayingPrompts = 4f;

        public override void StartTask()
        {
            base.StartTask();
            StartCoroutine(ShowRemedyingItemsAfterDelay());
            StartCoroutine(DisplayPromptsAfterDelay());
            teleportArea.enabled = true;
            teleportationProvider.endLocomotion += OnTeleportCompleted;
        }

        IEnumerator ShowRemedyingItemsAfterDelay()
        {
            yield return new WaitForSeconds(delayBeforeShowingRemedyingItems);
            remedyingItems.SetActive(true);
        }

        IEnumerator DisplayPromptsAfterDelay()
        {
            yield return new WaitForSeconds(delayBeforeDisplayingPrompts);
            TutorialXRControllersManager.Instance.SetControllersVisible(true);
            TutorialXRControllersManager.Instance.HighlightAndAnimateThumbsticks();
        }

        void OnTeleportCompleted(LocomotionSystem teleportationSystem)
        {
            Vector3 vectorToTarget = targetPoint.position - Camera.main.transform.position;
            float distanceToTarget = vectorToTarget.magnitude;
            if (distanceToTarget < thresholdDistanceFromTarget)
            {
                tutorialStateMachine.TriggerValidationFeedback();
                TryInvokeTaskCompletedWithDelay(standardTaskCompletionDelay);
            }
        }

        public override void CloseTask()
        {
            TutorialXRControllersManager.Instance.SetControllersVisible(false);
            teleportationProvider.endLocomotion -= OnTeleportCompleted;
        }
    }
}
