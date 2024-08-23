using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VFX;
using UnityEngine.XR.Interaction.Toolkit;
using VRTutorial;

public class LobbyTutorialManager : TutorialStateMachine
{
    public XRRayInteractor[] rayInteractors;
    public ParticleSystemsController confirmationEffectPrefab;
    public FireworksController fireworksController;
    public FireworksController SecondFireworksController;
    public Image validationIcon;
    public GameObject desk;
    public GameObject teleportAnchorParent;
    public AudioSource audioSource;
    public AudioClip validationClip;
    public GameObject experiencePanelsParent;
    public NarrationSO returnToLobbyNarration;
    public NarrationSO endOfGameNarration;
    public NarrationSO requestFeedbackNarration;
    public Button skipButton;
    public enum GameProgress { Beginner, VRSurveyorComplete, MakeUsSafeComplete };
    public static bool isFirstTimeOpened = true;

    public static LobbyTutorialManager Instance;

    protected override void Awake()
    {
        base.Awake();
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        ShowTeleportAnchors(false);
        GameProgress gameProgress = GetCurrentGameProgress();
        switch (gameProgress)
        {
            case GameProgress.Beginner:
                SetFirstTask();
                break;
            case GameProgress.VRSurveyorComplete:
                SkipTutorial();
                PlayReturnToLobbyNarration();
                break;
            case GameProgress.MakeUsSafeComplete:
                SkipTutorial();
                PlayEndOfGameNarration();
                break;
            default:
                break;
        }
    }

    GameProgress GetCurrentGameProgress()
    {
        bool vrSurveyorComplete = false;//!GameManager.LoadIntro;
        bool makeUsSafeComplete = false;// !Experience2Manager.LoadIntro;
        if (makeUsSafeComplete)
        {
            return GameProgress.MakeUsSafeComplete;
        }
        else if (vrSurveyorComplete)
        {
            return GameProgress.VRSurveyorComplete;
        }
        else
        {
            return GameProgress.Beginner;
        }
    }

    public override void SetTask(TutorialTask task)
    {
        base.SetTask(task);
        ResetValidationIcon();
    }

    private void PlayReturnToLobbyNarration()
    {
        StartCoroutine(PlayReturnToLobbyNarrationWithFireworks());
    }

    IEnumerator PlayReturnToLobbyNarrationWithFireworks()
    {
        narrator.ClearNarration();
        yield return new WaitForSeconds(1f);
        fireworksController.TriggerWaveOfFireworks();
        float fireworksDuration = fireworksController.intervalBetweenFireworks * fireworksController.numberOfFireworks;
        yield return new WaitForSeconds(fireworksDuration + 1f);
        PlayNarrationWith(returnToLobbyNarration);
    }

    private void PlayEndOfGameNarration()
    {
        StartCoroutine(PlayEndOfGameNarrationsWithFireworks());
    }

    IEnumerator PlayEndOfGameNarrationsWithFireworks()
    {
        PlayNarrationWith(endOfGameNarration);
        yield return new WaitForSeconds(endOfGameNarration.audio.length);
        fireworksController.TriggerWaveOfFireworks();
        SecondFireworksController.TriggerWaveOfFireworks();
        float fireworksDuration = fireworksController.intervalBetweenFireworks * fireworksController.numberOfFireworks;
        yield return new WaitForSeconds(fireworksDuration + 1f);
        PlayNarrationWith(requestFeedbackNarration);
    }

    private void PlayNarrationWith(NarrationSO narration)
    {
        narrator.ClearNarration();
        narrator.SetNarration(narration);
        narrator.PlayNarration();
    }


    public void UnhighlightAllButtons()
    {
        TutorialXRControllersManager.Instance.UnhighlightAllButtons();
    }

    public void SetControllersVisible(bool isControllerVisible)
    {
        TutorialXRControllersManager.Instance.SetControllersVisible(isControllerVisible);
    }

    public override void TriggerValidationFeedback()
    {
        audioSource.PlayOneShot(validationClip);
        validationIcon.color = Color.green;
    }

    public void SpawnVisualEffectAt(Vector3 spawnPosition)
    {
        ParticleSystemsController newConfirmationEffect = Instantiate(confirmationEffectPrefab, spawnPosition, Quaternion.identity);
        newConfirmationEffect.Play();
    }

    void ResetValidationIcon()
    {
        validationIcon.color = Color.gray;
    }

    public void RestartTutorial()
    {
        SetFirstTask();
    }

    public void SetRayInteractorsEnabled(bool isEnabled)
    {
        foreach (XRRayInteractor interactor in rayInteractors)
        {
            interactor.gameObject.SetActive(isEnabled);
        }
    }

    public void ShowDesk(bool shouldShow)
    {
        if (desk.activeInHierarchy != shouldShow)
        {
            desk.SetActive(shouldShow);
        }
    }

    public void ShowTeleportAnchors(bool shouldShow)
    {
        teleportAnchorParent.gameObject.SetActive(shouldShow);
    }

    public override void SkipTutorial()
    {
        base.SkipTutorial();
        DeactivateSkipButton();
        TutorialTask lastTask = tutorialTasks[tutorialTasks.Count - 1];
        lastTask.CloseTask();
    }

    public void DeactivateSkipButton()
    {
        skipButton.gameObject.SetActive(false);
    }

}