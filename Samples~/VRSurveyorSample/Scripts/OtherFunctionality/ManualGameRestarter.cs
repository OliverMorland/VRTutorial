using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;

public class ManualGameRestarter : MonoBehaviour
{
    public InputActionReference RestartAction;
    public UnityEvent OnRestartAction;
    public enum RestartState { UserNotReady, UserReady }
    static RestartState currentRestartState = RestartState.UserReady;
    [SerializeField] GameObject[] UserReadyObjects;
    [SerializeField] GameObject[] UserNotReadyObjects;

    void Awake()
    {
        RestartAction.action.performed += OnRestartActionPerformed;
        if (currentRestartState == RestartState.UserReady)
        {
            foreach (GameObject obj in UserReadyObjects)
            {
                obj.SetActive(true);
            }
            foreach (GameObject obj in UserNotReadyObjects)
            {
                obj.SetActive(false);
            }
        }
        else if (currentRestartState == RestartState.UserNotReady) 
        {
            foreach (GameObject obj in UserReadyObjects)
            {
                obj.SetActive(false);
            }
            foreach (GameObject obj in UserNotReadyObjects)
            {
                obj.SetActive(true);
            }
        }
    } 

    private void OnDestroy()
    {
        RestartAction.action.performed -= OnRestartActionPerformed;
    }

    private void OnRestartActionPerformed(InputAction.CallbackContext obj)
    {
        if (OnRestartAction != null)
        {
            OnRestartAction.Invoke();
        }
        RestartGame();
    }

    void RestartGame()
    {
        ToggleRestartState();
        LobbyTutorialManager.isFirstTimeOpened = true;
        //Experience2Manager.LoadIntro = true;
        //GameManager.LoadIntro = true;
        StartCoroutine(FadeAndLoadFirstScene());
    }

    void ToggleRestartState()
    {
        if (currentRestartState == RestartState.UserReady)
        {
            currentRestartState = RestartState.UserNotReady;
        }
        else
        {
            currentRestartState = RestartState.UserReady;
        }
    }

    IEnumerator FadeAndLoadFirstScene()
    {
        FadeScreens.Instance.FadeOut();
        float duration = FadeScreens.Instance.fadeDuration;
        yield return new WaitForSeconds(duration);
        SceneManager.LoadScene(0);
    }

}
