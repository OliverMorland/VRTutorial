using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VRTutorial
{
    public class WaitingTask : TutorialTask
    {
        public override void StartTask()
        {
            Debug.Log("Starting Waiting Task");
            base.StartTask();
            tutorialStateMachine.narrator.onNarrationFinished.AddListener(OnNarrationFinished);
        }

        void OnNarrationFinished()
        {
            TryInvokeTaskCompletedWithDelay(standardTaskCompletionDelay);
            tutorialStateMachine.narrator.onNarrationFinished.RemoveListener(OnNarrationFinished);
        }

        public override void CloseTask()
        {
            tutorialStateMachine.narrator.onNarrationFinished.RemoveListener(OnNarrationFinished);
        }
    }
}
