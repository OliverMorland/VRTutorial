using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace VRTutorial
{
    public class ClickUIButtonTask : TutorialTask
    {
        public Button uiButton;

        public override void StartTask()
        {
            base.StartTask();
            uiButton.onClick.AddListener(OnButtonClick);
        }

        void OnButtonClick()
        {
            tutorialStateMachine.TriggerValidationFeedback();
            uiButton.onClick.RemoveListener(OnButtonClick);
            TryInvokeTaskCompletedWithDelay(standardTaskCompletionDelay);
        }

        public override void CloseTask()
        {
            uiButton.onClick.RemoveListener(OnButtonClick);
        }
    }
}
