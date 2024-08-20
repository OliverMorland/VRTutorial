using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VRTutorial
{
    public class LookAtTask : TutorialTask
    {
        public Transform targetTransform;
        [Range(-0.95f, 0.95f)] public float viewAngleThreshold = 0.8f;
        [Range(0.2f, 2f)] public float requiredStareDuration = 1f;
        float currentStareDuration = 0f;
        Transform cameraTransform;
        bool isCheckingViewDirection = false;

        public override void StartTask()
        {
            base.StartTask();
            cameraTransform = Camera.main.transform;
            isCheckingViewDirection = true;
        }

        private void Update()
        {
            if (isCheckingViewDirection && targetTransform != null)
            {
                Vector3 cameraToTargetDir = (targetTransform.position - cameraTransform.position).normalized;
                float dot = Vector3.Dot(cameraTransform.forward, cameraToTargetDir);
                if (dot > viewAngleThreshold)
                {
                    currentStareDuration += Time.deltaTime;
                    if (currentStareDuration > requiredStareDuration)
                    {
                        TryInvokeTaskCompleted();
                    }
                }
                else
                {
                    currentStareDuration = 0;
                }
            }
        }

        public override void CloseTask()
        {
            isCheckingViewDirection = false;
        }
    }
}
