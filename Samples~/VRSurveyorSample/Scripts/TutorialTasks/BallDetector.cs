using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

namespace VRTutorial.Samples
{
    public class BallDetector : MonoBehaviour
    {
        public string targetTag;
        public int targetNumberOfBalls = 3;
        public List<GameObject> enteredBalls;
        public UnityEvent onTargetBallCountReached;
        public UnityEvent onBallDroppedInZone;

        private void Start()
        {
            enteredBalls = new List<GameObject>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == targetTag)
            {
                GameObject ball = other.gameObject;
                if (IsBallReleased(ball))
                {
                    if (IsBallNotYetEntered(ball))
                    {
                        enteredBalls.Add(other.gameObject);
                        TryInvokeEvent(onBallDroppedInZone);
                    }
                    if (enteredBalls.Count >= targetNumberOfBalls)
                    {
                        TryInvokeEvent(onTargetBallCountReached);
                    }
                }
            }
        }

        bool IsBallNotYetEntered(GameObject ball)
        {
            if (!enteredBalls.Contains(ball))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void OnDisable()
        {
            foreach (var ball in enteredBalls)
            {
                ball.gameObject.SetActive(false);
            }
        }

        private void OnEnable()
        {
            foreach (var ball in enteredBalls)
            {
                ball.gameObject.SetActive(true);
            }
        }

        void TryInvokeEvent(UnityEvent unityEvent)
        {
            if (unityEvent != null)
            {
                unityEvent.Invoke();
            }
        }

        bool IsBallReleased(GameObject ball)
        {
            XRGrabInteractable grabInteracteable = ball.GetComponent<XRGrabInteractable>();
            if (grabInteracteable != null)
            {
                if (grabInteracteable.isSelected)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
