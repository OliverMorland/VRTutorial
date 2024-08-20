using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace VRTutorial
{
    public class TeleportAreaTask : TutorialTask
    {
        public TeleportationArea teleportArea;
        public float delayBeforeDisplayingPrompts = 2f;

        public override void StartTask()
        {
            base.StartTask();
            teleportArea.enabled = true;
            teleportArea.teleporting.AddListener(OnTeleporting);
            StartCoroutine(DisplayPromptsAfterDelay());
        }

        IEnumerator DisplayPromptsAfterDelay()
        {
            yield return new WaitForSeconds(delayBeforeDisplayingPrompts);
            TutorialXRControllersManager.Instance.SetControllersVisible(true);
            TutorialXRControllersManager.Instance.HighlightAndAnimateThumbsticks();
        }

        void OnTeleporting(TeleportingEventArgs args)
        {
            teleportArea.teleporting.RemoveListener(OnTeleporting);
            tutorialStateMachine.TriggerValidationFeedback();
            TryInvokeTaskCompletedWithDelay(standardTaskCompletionDelay);
        }

        public override void CloseTask()
        {
            teleportArea.teleporting.RemoveListener(OnTeleporting);
            TutorialXRControllersManager.Instance.SetControllersVisible(false);
        }
    }
}
