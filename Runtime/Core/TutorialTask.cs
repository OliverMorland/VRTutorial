using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace VRTutorial
{
    public abstract class TutorialTask : MonoBehaviour
    {
        public NarrationSO taskInstructionsNarration;
        public UnityEvent OnTaskCompleted;
        protected TutorialStateMachine tutorialStateMachine;
        protected const float standardTaskCompletionDelay = 1f;
        bool taskCompleted;

        public virtual void StartTask()
        {
            taskCompleted = false;
            tutorialStateMachine.ClearNarration();
            if (taskInstructionsNarration != null)
            {
                tutorialStateMachine.PlayNarration(taskInstructionsNarration);
            }
        }

        public void TryInvokeTaskCompleted()
        {
            if (OnTaskCompleted != null)
            {
                if (!taskCompleted)//This stops task completion being invoked multiple times.
                {
                    Debug.Log("Task Completed " + gameObject.name);
                    OnTaskCompleted.Invoke();
                    taskCompleted = true;
                }
            }
        }

        IEnumerator WaitForDelayBeforeInvokingTaskCompleted(float delay)
        {
            yield return new WaitForSeconds(delay);
            TryInvokeTaskCompleted();
        }

        public void TryInvokeTaskCompletedWithDelay(float delay)
        {
            StartCoroutine(WaitForDelayBeforeInvokingTaskCompleted(delay));
        }

        public abstract void CloseTask();

        public void SetManager(TutorialStateMachine _tutorialStateMachine)
        {
            tutorialStateMachine = _tutorialStateMachine;
        }
    }
}
