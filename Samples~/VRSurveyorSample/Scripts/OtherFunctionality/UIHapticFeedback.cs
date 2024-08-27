using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.UI;

public class UIHapticFeedback : MonoBehaviour, IPointerEnterHandler
{
    public float duration = 0.05f;
    public float amplitude = 0.1f;
    private XRUIInputModule inputModule => EventSystem.current.currentInputModule as XRUIInputModule;


    public void OnPointerEnter(PointerEventData eventData)
    {
        XRRayInteractor interactor = inputModule.GetInteractor(eventData.pointerId) as XRRayInteractor;
        if (interactor != null) 
        {
            interactor.xrController.SendHapticImpulse(amplitude, duration);
        }
        
    }
}
