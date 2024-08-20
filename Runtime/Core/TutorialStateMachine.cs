using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace VRTutorial
{
    public abstract class TutorialStateMachine : MonoBehaviour
    {
        public Narrator narrator;
        public List<TutorialTask> tutorialTasks;
        public TutorialTask startTask;
        public TutorialTask currentTask;
        public UnityEvent OnTutorialComplete;

        protected virtual void Awake()
        {
            SetTasksList();
            SetUpTaskCompletionListeners();
        }

        void SetTasksList()
        {
            TutorialTask[] childTutorialTasks = GetComponentsInChildren<TutorialTask>();
            foreach (TutorialTask tutorialTask in childTutorialTasks)
            {
                tutorialTasks.Add(tutorialTask);
                tutorialTask.SetManager(this);
            }
        }

        public void SetFirstTask()
        {
            if (startTask != null)
            {
                SetTask(startTask);
            }
            else
            {
                SetTask(tutorialTasks[0]);
            }
        }

        private void SetUpTaskCompletionListeners()
        {
            foreach (TutorialTask lobbyTutorialTask in tutorialTasks)
            {
                lobbyTutorialTask.OnTaskCompleted.AddListener(StartNextTask);
            }
        }

        public void StartNextTask()
        {
            int currentTaskIndex = tutorialTasks.IndexOf(currentTask);
            int nextTaskIndex = currentTaskIndex + 1;
            if (nextTaskIndex < tutorialTasks.Count)
            {
                TutorialTask nextTask = tutorialTasks[nextTaskIndex];
                SetTask(nextTask);
            }
            else
            {
                TryInvokeOnTutorialComplete();
            }
        }

        private void TryInvokeOnTutorialComplete()
        {
            if (OnTutorialComplete != null)
            {
                OnTutorialComplete.Invoke();
            }
        }

        public virtual void SetTask(TutorialTask task)
        {
            CloseAllTasks();
            currentTask = task;
            currentTask.StartTask();
        }

        private void CloseAllTasks()
        {
            foreach (var tutorialTask in tutorialTasks)
            {
                tutorialTask.CloseTask();
            }
        }

        public void ClearNarration()
        {
            narrator.ClearNarration();
        }

        public void PlayNarration(NarrationSO narration)
        {
            narrator.SetNarration(narration);
            narrator.PlayNarration();
        }

        public virtual void SkipTutorial()
        {
            if (tutorialTasks.Count > 0)
            {
                TutorialTask lastTask = tutorialTasks[tutorialTasks.Count - 1];
                SetTask(lastTask);
            }
        }

        public abstract void TriggerValidationFeedback();
    }
}
