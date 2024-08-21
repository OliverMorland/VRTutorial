using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.VFX;

namespace VRTutorial.Samples
{
    public abstract class LobbyTutorialTask : TutorialTask
    {
        protected LobbyTutorialManager lobbyTutorialManager;

        private void Start()
        {
            lobbyTutorialManager = LobbyTutorialManager.Instance;
        }

        public override void StartTask()
        {
            lobbyTutorialManager = LobbyTutorialManager.Instance;
            base.StartTask();
        }
    }
}
