using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR;

public class PlayerTransformResetter : MonoBehaviour
{
    [SerializeField] Transform targetTransform;
    [SerializeField] XROrigin xrOrgin;
    [SerializeField] Camera userView;
    [SerializeField] bool ResetOnStart;

    public void Start()
    {
        if (ResetOnStart)
        {
            //Waiting for camera orientation to be initialized first.
            StartCoroutine(WaitBeforeResettingRotation());
        }
    }

    public void ResetPositionAndRotation()
    {
        ResetPosition();
        ResetRotation();
    }

    IEnumerator WaitBeforeResettingRotation()
    {
        yield return new WaitForEndOfFrame();
        ResetPositionAndRotation();
    }

    public void ResetRotation()
    {
        float rotationAngleY = targetTransform.rotation.eulerAngles.y - userView.transform.rotation.eulerAngles.y;
        xrOrgin.transform.Rotate(0, rotationAngleY, 0);
    }

    public void ResetPosition()
    {
        xrOrgin.transform.position = targetTransform.position;
    }
}
